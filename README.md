# 🎹 AR Children's Song Piano

> 🎓 **Immersive Media Midterm Project**  
> Interactive Music Learning App for Kids

## 📖 Project Overview

An **educational music learning app** where children can point their camera at picture books to reveal an AR piano.

**🎯 Target Audience**: Ages 5-10 (Music Education for Kids)

**Core Values**:
- 📚 Combining picture books with AR technology
- 🎵 Learning basic musical notes (Do, Re, Mi, Fa, Sol, La, Si)
- 👆 Touch interactions with visual feedback
- ⏱️ Self-paced learning at each child's comfortable speed

---

## 🎬 Demo Video

[![AR Piano Learning Game Demo](https://i.ibb.co/jPbBB9FS/appicon.png)](https://youtube.com/shorts/iaHR3xAzIWU)

*Click the image above to watch the demo video!*

---

## 🎮 How to Use

### 1️⃣ App Launch & Tutorial
When you launch the app, an **Info Popup** appears to guide you through the features.
- Welcome page with "See Songs!" button to view available songs
- Introduction cards for each children's song (Airplane, Twinkle Twinkle Little Star, Butterfly)
- "Let's Play!" button to start the game

### 2️⃣ Image Recognition
Point the camera at a children's song picture card.
- 📖 **Image Detection** → AR piano keyboard appears overlaid on the book
- 🎼 The **song title** and **sheet music** are displayed on screen

### 3️⃣ Piano Playing
Follow the sheet music and press the piano keys in order.
- ✅ **Correct**: Green checkmark (✓) → Progress to next note
- ❌ **Wrong**: Red X mark (✗) → Try again

### 4️⃣ Song Completion
When all notes are played correctly, a congratulations popup appears!
- 🔄 **Retry**: Challenge the same song again
- ❌ **Close**: Select a different song

---

## ✨ Key Features

### 🎯 AR Image Recognition
- Uses **XR Reference Image Library**
- Supports **3 children's song picture cards**:
  - ✈️ Airplane
  - ⭐ Twinkle Twinkle Little Star  
  - 🦋 Butterfly
- Automatically displays the corresponding piano keyboard upon image recognition

### 🎹 Interactive Piano
- **7-note color keyboard**: C(Do), D(Re), E(Mi), F(Fa), G(Sol), A(La), B(Si)
- Each key distinguished by its own unique color
- Real piano sound plays when touched
- Sheet music and keyboard displayed together on screen

### 📊 Real-time Feedback
- **Immediate visual feedback**:
  - ✅ Correct: Green checkmark
  - ❌ Wrong: Red X mark
- Sheet music progress indicator
- Automatic transition to next line upon line completion

### 🎨 Intuitive UI/UX
- **Info Popup**: 4-page tutorial system
  - Page 1: Welcome & "See Songs!" button
  - Page 2-4: Individual song introduction cards
  - Next/Back buttons for page navigation
  - "Let's Play!" button on final page
- **Success Popup**: Congratulations message upon song completion
- **Top buttons**: Info, Retry, Exit

---

## 🛠 Tech Stack

### Development Environment
- **Unity 2021.2+**
- **AR Foundation 5.x**
- **ARKit** (iOS)
- **TextMeshPro** (UI Text)

### Main Components
- `AR Tracked Image Manager` - Image recognition
- `AR Session Origin` - AR space management
- `Canvas (HUD)` - Fixed UI elements

---

## 📁 Project Structure

```
Unity-AR-Project/
├── Assets/
│   ├── Scenes/                    # AR Piano main scene
│   ├── Scripts/                   # Game logic scripts
│   │   ├── FinalImageTracker.cs   # AR image recognition management
│   │   ├── GameManager.cs         # Game progression logic
│   │   ├── PianoKey.cs            # Piano key input
│   │   ├── SheetMusicInfo.cs      # Sheet music data
│   │   └── UIManager.cs           # UI popup management
│   ├── Images/                    # Children's song picture cards
│   │   ├── airplane.png
│   │   ├── star.png
│   │   └── butterfly.png
│   ├── Audio/                     # Piano sounds
│   ├── UI/                        # UI images & icons
│   └── Prefabs/                   # Sheet music prefabs
├── ContentPackages/               # AR image library
└── ProjectSettings/               # Unity project settings
```

---

## 🎼 Available Songs

✈️ **Airplane** 
⭐ **Twinkle Twinkle Little Star** 
🦋 **Butterfly** 

---

## 📝 Development Notes

### Design Philosophy
1. **Educational Value**: Learn music theory in a fun way
2. **Intuitive UX**: Interface understandable without instructions
3. **Extensibility**: Easy to add new songs and features

---

## 📄 License

This project was created for educational purposes.
