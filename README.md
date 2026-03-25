# FridgeBuddy

FridgeBuddy is a desktop inventory tracker built with WPF, C#, 
and MVVM that helps users manage drinks in a virtual fridge, 
including quantities, serving sizes, and quick stock updates

---

## Features

- Add drinks to the inventory
- Update or delete drinks
- Quick quantity adjustments (+1 / -1)
- Custom quantity adjustments
- Restock functionality
- Persistent storage using AppData (JSON)

---

## Technologies

- C#
- WPF
- MVVM architecture
- JSON (AppData persistence)
- JetBrains Rider

---

## Architecture

The application follows the MVVM (Model-View-ViewModel) pattern:

- Model: Represents the data structure of beers  
- ViewModel: Handles logic, commands, and state  
- View: WPF UI with data binding  

Commands are implemented to manage user interactions such as adding, updating, and removing items.

---

## Screenshots

Main UI

<img width="879" height="438" alt="FridgeBuddyMain" src="https://github.com/user-attachments/assets/fb3b02c9-29db-4fac-a1d0-a1b85b696075" />

Selected drink

<img width="877" height="441" alt="FridgeBuddySelected" src="https://github.com/user-attachments/assets/442b312b-d0a4-4cc2-a298-1e28f3fe05cb" />

Add view

<img width="403" height="250" alt="FridgeBuddyAddView" src="https://github.com/user-attachments/assets/824eac78-383d-429c-ac57-72d0c1fb4d34" />

---

## Motivation

It started as a simple personal beer inventory app, then I broadened the concept into a more general beverage/fridge inventory tool. 
The main goal was to build a structured desktop application using WPF and MVVM.

---

## Future Improvements

- Support for bottle/can images
- Low stock alerts
- Sorting and filtering options
- Potential web version (Vue.js and Laravel)

---
