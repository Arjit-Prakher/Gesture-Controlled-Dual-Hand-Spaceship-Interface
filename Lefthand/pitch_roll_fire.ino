#include "Wire.h"
#include <MPU6050_light.h>


const int FIRE_BUTTON_PIN = 2; 

MPU6050 mpu(Wire);

long timer = 0;
const long updateInterval = 10;

float yawAngle = 0.0; // Variable to store the integrated yaw angle

// Offsets for the three axes when the sensor is flat
float accX_offset = 0.0;
float accY_offset = 0.0;
float accZ_offset = 0.0;
bool calibrated = false;
int calibrationSamples = 1000;

void setup() {
  Serial.begin(115200);
  Wire.begin();

  byte status = mpu.begin();
  while(status != 0) { /* handle error */ }

  Serial.println(F("Calibrating Left Hand Sensor, keep FLAT and STILL!"));
  // Calibrate Accelerometer (for pitch and roll)
  for(int i=0; i < calibrationSamples; i++) {
    mpu.update();
    accX_offset += mpu.getAccX();
    accY_offset += mpu.getAccY();
    accZ_offset += mpu.getAccZ();
    delay(5);
  }
  accX_offset /= calibrationSamples;
  accY_offset /= calibrationSamples;
  accZ_offset /= calibrationSamples;
  calibrated = true;
  
  Serial.println("Calibration complete!\n");
  timer = millis();
  pinMode(FIRE_BUTTON_PIN, INPUT_PULLUP);
}

void loop() {
  mpu.update();

  if (digitalRead(FIRE_BUTTON_PIN) == LOW) {
    // Print the firing message for Unity to read
    Serial.println("FIRE!"); 
    // Small delay to handle "debouncing" (prevent multiple rapid reads from one press)
    delay(100); 
  }
  if (calibrated && millis() - timer > updateInterval) {
    // float deltaTime = (millis() - timer) / 1000.0; // Time in seconds

    // 1. PITCH (Tilt Up/Down) - Use Y-Axis Accelerometer
    float pitchInput = mpu.getAccY() - accY_offset;

    // 2. ROLL (Side-to-Side) - Use X-Axis Accelerometer
    float rollInput = mpu.getAccX() - accX_offset;

    // Output format: "PITCH,YAW,ROLL"
    Serial.print(pitchInput);
    Serial.print(",");
    Serial.println(rollInput);

    timer = millis();
  }
}