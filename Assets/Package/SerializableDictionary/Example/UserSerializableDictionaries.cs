﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class StringStringDictionary : SerializableDictionary<string, string> {}

[Serializable]
public class ObjectColorDictionary : SerializableDictionary<UnityEngine.Object, Color> {}

[Serializable]
public class ColorArrayStorage : SerializableDictionary.Storage<Color[]> {}

[Serializable]
public class StringColorArrayDictionary : SerializableDictionary<string, Color[], ColorArrayStorage> {}

[Serializable]
public class MyClass
{
    public int i;
    public string str;
}

[Serializable]
public class QuaternionMyClassDictionary : SerializableDictionary<Quaternion, MyClass> {}

[System.Serializable]
public class SoundEffects : SerializableDictionary<string, AudioClip> {};
[System.Serializable]
public class SoundSources : SerializableDictionary<string, AudioSource> {};
[System.Serializable]
public class SerialilzeDicStringVector3 : SerializableDictionary<string, Vector3>{}