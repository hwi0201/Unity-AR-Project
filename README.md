# 🎹 AR Piano Learning Game

> 🎓 **Immersive Media Midterm Project**  
> Interactive Piano Learning Game Using AR Image Tracking Technology

## 📖 Project Overview

An **AR music education game** that recognizes physical image markers to display 3D sheet music and allows users to play songs using virtual piano keys.

- 🎯 **Point your camera at a specific image** → Sheet music for that song appears in AR
- 🎹 **Press the on-screen piano keys in order** → Play the song
- ✅ **Press the correct note** → Visual feedback and progress to the next note
- 🎉 **Complete the song** → Success popup with retry option

---

## 🎮 How to Play

### Step 1: Image Marker Recognition
Point your camera at a specific image (e.g., star, airplane) to generate 3D sheet music in AR space.

### Step 2: Follow the Sheet Music
Press the piano keys at the bottom of the screen in the order shown on the sheet music.
- ✅ **Correct**: Green checkmark → Progress to next note
- ❌ **Wrong**: Red X mark → Try again

### Step 3: Song Completion
When all notes are played correctly, a success popup appears, allowing you to retry or select another song.

---

## 🛠 Tech Stack

### Development Environment
| Technology | Version/Description |
|------------|---------------------|
| **Unity** | 6000.0 37f1 |
| **AR Foundation** | 5.x |
| **AR Tracked Image Manager** | Image tracking |
| **TextMeshPro** | UI text rendering |

### Platform Support
- 📱 **iOS** (ARKit)
- 🤖 **Android** (ARCore)

---

## 📁 Project Structure

```
Unity-AR-Project/
├── Assets/
│   ├── Scenes/                    # Main AR scenes
│   ├── Scripts/                   # C# scripts
│   │   ├── FinalImageTracker.cs   # AR image recognition & object spawning
│   │   ├── GameManager.cs         # Game logic & song progression
│   │   ├── PianoKey.cs            # Piano key input handling
│   │   ├── SheetMusicInfo.cs      # Sheet music data structure
│   │   └── UIManager.cs           # UI popup & page management
│   ├── Prefabs/                   # 3D sheet music prefabs
│   ├── Materials/                 # Materials & textures
│   ├── Audio/                     # Piano sound files
│   └── UI/                        # UI images & icons
├── ContentPackages/               # AR image library
├── Packages/                      # Unity package dependencies
└── ProjectSettings/               # Project settings
```

---

## ✨ Key Features

### 🎯 AR Image Tracking
- **Multiple Image Recognition**: Recognize multiple image markers simultaneously
- **Dynamic Object Placement**: Real-time 3D sheet music placement on recognized markers
- **Tracking State Management**: Automatic object hiding when marker is lost

### 🎹 Interactive Piano System
- **7-Note Scale**: Do, Re, Mi, Fa, Sol, La, Si
- **Real-time Sound Feedback**: Piano tone playback for each key
- **Visual Feedback**: 
  - ✅ Correct: Green checkmark
  - ❌ Wrong: Red X mark

### 📊 Game Progress Management
- **Automatic Page Turning**: Auto-transition to next line on completion
- **Progress State Saving**: Track current note and line being played
- **Song Completion Detection**: Success popup after final note

### 🎨 UI/UX
- **Tutorial System**: Usage guide on app start
- **Song Selection Screen**: Display available songs
- **Success Popup**: Congratulations message and retry option on completion
- **Feedback Animation**: Feedback displays for 0.15 seconds then auto-hides

---

## 📝 Development Notes

### Design Philosophy
1. **Educational Value**: Learn music theory in a fun way
2. **Intuitive UX**: Interface understandable without instructions
3. **Extensibility**: Easy to add new songs and features

### Future Improvements
- [ ] Difficulty-based song classification system
- [ ] Performance accuracy scoring system
- [ ] Leaderboard feature
- [ ] More songs (nursery rhymes, K-POP, etc.)
- [ ] Rhythm game mode
- [ ] Multiplayer functionality

---

## 📄 License

This project was created for educational purposes.
