# 🚀 Gesture-Controlled Spaceship Interface

A personal project exploring immersive control systems using dual MPU6050 sensors and Arduino Nano microcontrollers. This Unity-based simulation translates real-world hand gestures into thrust, orientation, and weapon control—laying the foundation for intuitive, body-driven gameplay mechanics.

## 🎯 Project Goals

- Bring sci-fi-inspired control ideas to life
- Explore gesture-based input for VR and simulation games
- Prototype a modular, low-cost cockpit interface using real-time sensor data

## 🧩 Features

- Dual-hand gesture control using two MPU6050 sensors
- Real-time thrust and rotation mapping in Unity 3D
- Push-button weapon firing
- Complementary averaging and smooth motion
- Modular architecture for future expansion

## 🛠️ Hardware Setup

- 2× MPU6050 Accelerometer + Gyroscope sensors
- 2× Arduino Nano microcontrollers
- 1× Push button (for weapon firing)
- Jumper wires, breadboards, USB cables

### 📐 Diagrams

- Wiring diagrams for each hand
- Sensor orientation illustrations
- System overview block diagram
- Gesture mapping flowchart  
*(See `/docs` folder or embedded images)*

## 🧠 Gesture Mapping

| Hand       | Gesture                  | Action             |
|------------|--------------------------|--------------------|
| Right      | Tilt forward/backward    | Thrust forward/back |
| Left       | Wrist tilt up/down       | Pitch              |
| Left       | Slow horizontal movement | Yaw                |
| Left       | Fast horizontal flick    | Roll               |
| Left       | Push button press        | Fire weapon        |

## 🧪 Calibration & Filtering

- Complementary filter blends accelerometer and gyroscope data
- Calibration sets neutral orientation baseline
- Thresholds and smoothing applied to reduce jitter

## 🧱 Software Architecture

- Unity 3D for simulation and rendering
- C# scripts for movement, rotation, and input parsing
- Serial communication from Arduino to Unity
- Modular control scripts for each hand

## ▶️ How to Run

1. Upload Arduino sketches to both Nano boards
2. Connect sensors and button as per wiring diagrams
3. Open Unity project in Unity 2022.3 or later
4. Set correct serial ports in Unity scripts
5. Press Play to begin simulation

## 🚧 Known Issues

- Sensor drift under extreme motion
- Requires stable mounting for accurate gesture detection

## 🌌 Future Improvements

- VR headset integration
- Multiplayer cockpit simulation
- Gesture-based UI interactions

## 📘 Conclusion

This project demonstrates how low-cost hardware and creative engineering can unlock new modes of interaction—bridging the gap between physical motion and digital response. It stands as a testament to curiosity, persistence, and the power of hands-on experimentation.

## 👤 Author

**Arjit**  
Passionate about immersive systems, sensor fusion, and sci-fi-inspired control design.  
Feel free to connect or reach out for collaboration!

