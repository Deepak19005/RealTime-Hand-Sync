const int flexPin1 = A0;  // Analog pin for Flex Sensor 1
const int flexPin2 = A1;  // Analog pin for Flex Sensor 2
const int flexPin3 = A2;  // Analog pin for Flex Sensor 3

void setup() {
  Serial.begin(9600);
}

void loop() {
  // Read values from flex sensors
  int flexValue1 = analogRead(flexPin1);
  int flexValue2 = analogRead(flexPin2);
  int flexValue3 = analogRead(flexPin3);

  // Send values to Unity separated by commas
  Serial.print(flexValue1);
  Serial.print(",");
  Serial.print(flexValue2);
  Serial.print(",");
  Serial.println(flexValue3);

  delay(100);  // Adjust delay as needed for your application
}