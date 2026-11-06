# Furry Fury

Furry Fury is a 3D combat game prototype with cat characters, developed in Unity and C#. It is still in development.

## Intention

The main purpose of the project was to build a data-driven ability system, that is, a way to define characters, abilities and status effects as data and small classes, so new content can be added without modifying the core combat code. The visuals were never the priority: models, UI and scenes are rough and mostly placeholder.

## What was achieved

First, characters are defined as ScriptableObjects that group their stats and an ability set. Every ability inherits from a common `Ability` class and implements a single `Execute` method, which means each one lives in its own file and its values (damage, radius, duration, etc.) can be tuned from the Inspector. Seven abilities were implemented: Dash, Scratch, Spin Attack, Heavy Attack, Block, Howl and Explosion.

Then comes the status-effect system, which handles damage and healing over time, stat changes and stuns, with stacking rules. Said effects can also be conditional, and Block is the clearest example: its damage reduction only lasts while the button is held. Finally, gameplay and UI are kept apart through event hubs and small interfaces such as `IDamageable` and `IStunnable`. All the code is under `Assets/Scripting`.

## Running the project

The project was developed with Unity 2022.3. To try it, open it with that version and press Play from `Assets/Scenes/MainMenu.unity`.

## Notes

This repository comes from a Unity Version Control (UVC) repository, which is why there is no traceable commit history here.
