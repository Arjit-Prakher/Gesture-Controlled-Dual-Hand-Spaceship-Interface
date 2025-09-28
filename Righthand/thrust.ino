#include "Wire.h"
#include <MPU6050_light.h>

MPU6050 mpu(Wire);

long timer = 0;

// Use a shorter update interval for smoother control
const long updateInterval = 10;  // in milliseconds (100 updates per second)

float zOffset = 0.0;
bool calibrated = false;
int calibrationSamples = 1000;

void setup() {
  // Use a higher baud rate for faster, more reliable communication
  Serial.begin(115200);
  Wire.begin();

  byte status = mpu.begin();
  Serial.print(F("MPU6050 status: "));
  Serial.println(status);
  while (status != 0) {}  // Stop everything if could not connect to MPU6050

  for (int i = 0; i < calibrationSamples; i++) {
    mpu.update();
    zOffset += mpu.getAccY();
    delay(5);
  }
  zOffset /= calibrationSamples;
  calibrated = true;
  Serial.print("Z-axis Offset: ");
  Serial.println(zOffset);
  Serial.println("Calibration complete!\n");



  // Serial.println(F("Calculating offsets, do not move MPU6050"));
  // delay(1000);
  // mpu.calcOffsets(true, true);  // gyro and accelero
  // Serial.println("Done!\n");

  // Set the timer to the current time to start the loop immediately
  timer = millis();
}

void loop() {
  mpu.update();

  if (calibrated && millis() - timer > updateInterval) {
    // Correct the Z-axis reading by subtracting the calibration offset
    float correctedAccZ = mpu.getAccY() - zOffset;

    // Send only the corrected Z-axis value to the Serial Monitor
    Serial.println(correctedAccZ);

    timer = millis();
  }
}


// void loop() {
//   mpu.update();

//   // Send data more frequently, as defined by updateInterval
//   if (millis() - timer > updateInterval) {
//     // Sending data in the following format "ax,ay,az,gx,gy,gz" separated by commas, with no extra text
//     // The mpu.getAccX(), getAccY(), and getAccZ() methods already return float values

//     Serial.print(mpu.getAccX());
//     Serial.print(",");
//     Serial.print(mpu.getAccY());
//     Serial.print(",");
//     Serial.print(mpu.getAccZ());
//     Serial.print(",");
//     Serial.print(mpu.getGyroX());
//     Serial.print(",");
//     Serial.print(mpu.getGyroY());
//     Serial.print(",");
//     Serial.println(mpu.getGyroZ());  // using println() for the last value to send a newline



//     timer = millis();
//   }
// }