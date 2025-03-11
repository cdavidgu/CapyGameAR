# 🏃‍♂️ AR Capybara Endless Runner  

## 📌 Project Overview  
This is an **endless runner game with augmented reality (AR)**. Players control a capybara running through an AR-generated environment, collecting food and avoiding natural predators like jaguars and alligators. Built with **Unity, C#, and AR Foundation**.  

## 🚀 Features  
- **📱 AR Experience** – The game **starts when the camera detects an image marker ([here](https://is.gd/fB5e9J))**, launching the AR arena.  
- **🐹 Play as a Capybara** – Control the character in an endless runner game, moving left and right to dodge predators and collect food.  
- **🔄 Game Restart** – If caught, choose between restarting or exiting the game. Play as many times as you want. 
- **📴 Fully Offline** – No internet connection required.  
- **📲 Available for iOS & Android**  

## 🏗️ Technology Stack  
- **Unity** – Game engine used for development.  
- **C#** – Core programming language.  
- **AR Foundation** – AR framework for image-based tracking.  

## 💻 Code & Architecture

The following architectural patterns were implemented:

**Singleton Pattern** – Used to manage the overall state of the app.

**Object Pooling Pattern** – Implemented to optimize enemy and reward instantiation, reducing memory overhead and improving performance.

**Entity-Component-System (ECS) pattern** - Used for maximizing Unity’s performance and leveraging its modular architecture design. 


## 📲 Installation & Setup
### Android
1. **Download the APK** ([here](https://is.gd/fB5e9J)).
2. Install the APK on an Android device (enable "Unknown Sources" if needed).
3. Open the app and explore its features!

### iOS
(Currently not publicly available; it requires manual deployment via Xcode.)

## 🎮 How It Works  
1. **Launch the Game** – Launch the game. Point the camera at the **image marker** to activate the AR experience ([Download image marker here](https://is.gd/fB5e9J)).  
2. **Start Running** – The capybara begins moving forward. The player can move **left or right** using HUD buttons.  
3. **Collect & Survive** – Grab food for points while dodging jaguars and alligators.  
4. **Game Over** – If the capybara gets caught, user can restart or exit.  

## 🔜 Future Improvements  
- **Power-ups** – Add special items for speed boosts or shields.  
- **Jumping Mechanic** – Introduce jump movement to avoid obstacles.  


