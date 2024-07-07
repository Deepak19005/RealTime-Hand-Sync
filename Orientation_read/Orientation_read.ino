#include "I2Cdev.h"
#include "MPU6050_6Axis_MotionApps20.h"

MPU6050 mpu;
Quaternion q;

volatile bool mpuInterrupt = false; // indicates whether MPU interrupt pin has gone high

bool dmpReady = false;     // set true if DMP init was successful
uint8_t mpuIntStatus;      // holds actual interrupt status byte from MPU
uint16_t packetSize;       // expected DMP packet size (default is 42 bytes)
uint8_t fifoBuffer[64];    // FIFO storage buffer

#define INTERRUPT_PIN 2  // use pin 2 on Arduino Uno & most boards
#define LED_PIN 13 // (Arduino is 13, Teensy is 11, Teensy++ is 6)
bool blinkState = false;

void dmpDataReady() {
    mpuInterrupt = true;
}

void setup() {
    Serial.begin(115200);
    while (!Serial);

    Serial.println("Initializing I2C devices...");
    mpu.initialize();
    
    pinMode(INTERRUPT_PIN, INPUT);

    Serial.println("Testing device connections...");
    Serial.println(mpu.testConnection() ? "MPU6050 connection successful" : "MPU6050 connection failed");

    Serial.println("Initializing DMP...");
    if (mpu.dmpInitialize() == 0) {
        mpu.CalibrateAccel(6);
        mpu.CalibrateGyro(6);
        mpu.setDMPEnabled(true);

        attachInterrupt(digitalPinToInterrupt(INTERRUPT_PIN), dmpDataReady, RISING);
        mpuIntStatus = mpu.getIntStatus();

        dmpReady = true;
        packetSize = mpu.dmpGetFIFOPacketSize();
    } else {
        Serial.println("DMP Initialization failed");
    }

    pinMode(LED_PIN, OUTPUT);
}

void loop() {
    if (!dmpReady) return;
    if (mpu.dmpGetCurrentFIFOPacket(fifoBuffer)) {
        mpu.dmpGetQuaternion(&q, fifoBuffer);
        Serial.print(q.w); Serial.print(","); Serial.print(q.x); Serial.print(",");
        Serial.print(q.y); Serial.print(","); Serial.println(q.z);

        // blink LED to indicate activity
        blinkState = !blinkState;
        digitalWrite(LED_PIN, blinkState);
        delay(100);
    }
}