PixeleffectTranss
art: benmhenry@gmail.com
code: davidahenry@gmail.com

Description:
Includes thirty five animated pixel effectTranss.<br>
Includes: Flame, Selection, Sick, Sleep, Path, Block, Box, Bubble, Circle, Claw, Consume, Dark, Earth, Electric, Explode, Fire, Footprints, Glint, Heal, Ice, Lightning, Nuclear, Poison, Puff, Shield, Slash, Sparks, Splatter, Square.<br>
Also includes simple animated slime.

Documentation:
0. Use effectTrans.prefab or:
1. Create empty game object, add Animator, select an effectTrans animation.
2. Create child (sprite or image) Offset with two children (sprite or image) Fore and Back.
3. Set Additive mat on sprite renderer if want.
4. You can read the recommended offset positions and additive mode from included data files as done in example.

Files:
PixeleffectTranss/Audio/PinDrop.wav : used for example scene intro
PixeleffectTranss/Data/Block.asset (etc) : positioning and blending data for all animations (ya probably a better way)
PixeleffectTranss/Prefab/effectTrans.prefab : can be used to instantiate any effectTrans
PixeleffectTranss/Prefab/Path.prefab : can be used to instantiate any simple looping effectTrans
PixeleffectTranss/Scene/effectTrans.unity : example scene
PixeleffectTranss/Scene/effectTranss.unity : example scene
PixeleffectTranss/Scene/Intro.unity : example scene intro
PixeleffectTranss/Script/Ease.cs : simple ease system used for bounce in example scene intro
PixeleffectTranss/Script/effectTranss.cs : used to manage and pool and spawn effectTranss
PixeleffectTranss/Script/effectTrans1.cs (etc) : groups a few similar effectTranss together on each mob for example scene
PixeleffectTranss/Script/Intro.cs : used for example scene intro
PixeleffectTranss/Script/ModeleffectTransAnimation.ca : ScriptableObject used to hold some position and blend data about animations
PixeleffectTranss/Script/Pool.cs : used to pool effectTranss or anything
PixeleffectTranss/Script/StateFlip.cs : used in animation controllers to flip some effectTranss randomly sometimes
PixeleffectTranss/Script/StateRandom.cs : used in animation controllers to select random animation
PixeleffectTranss/Script/StateRemove.cs : used to pool objects when done
PixeleffectTranss/Visual/Animation/effectTranss/Block.controller (etc) : effectTrans animation controllers
PixeleffectTranss/Visual/Animation/Slime.controller: slime animation controller used in example
PixeleffectTranss/Visual/Font/SuperBlack.fontsettings (etc) : fonts used in example
PixeleffectTranss/Visual/Sprite/effectTranss/Block.png (etc) : all sprites on single sprite sheet
PixeleffectTranss/Visual/Sprite/Henry.png : used in intro
PixeleffectTranss/Visual/Sprite/SlimeA.png : slime used in example
PixeleffectTranss/ReadMe.txt : this
