# RealTime-Hand-Sync

**Project Overview**  
An electronic glove that captures real-time hand motion and replicates it in Unity.  
The system uses **flex sensors** to measure individual finger bending and an **MPU6050** sensor to capture hand orientation. Sensor data is processed through an **Arduino UNO** and transmitted to **Unity**, where a 3D hand model mimics the actual hand movement live.

---

**Hardware Used**
- Arduino UNO  
- Flex Sensors (for finger bend detection)  
- MPU6050 (for orientation tracking)  
- Jumper Wires and Breadboard  

---

**Software Used**
- Arduino IDE  
- Unity 3D  
- Serial Communication Interface (C# Script in Unity)

---

**Working Principle**
1. Flex sensors output varying resistance with finger bend → converted to angle values.  
2. MPU6050 provides roll, pitch, and yaw for hand orientation.  
3. Arduino reads both sensor sets and sends combined data to Unity over serial communication.  
4. Unity hand model mirrors the real hand’s motion in real time.

---

**Applications**
- Virtual Reality hand tracking  
- Robotics control interfaces  
- Sign language recognition research  

---

**Contributors**
L. G. Deepak — IIT Ropar, Mechanical Engineering  

