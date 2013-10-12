#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class AudioPlayerEditor : ScriptableObject {

    [MenuItem ("Utilities/AudioPlayer/SortSoundList")]
    static void  SortSoundList() {
		GameObject obj = Selection.activeObject as GameObject;
		AudioPlayer audioPlayer = obj.GetComponent<AudioPlayer>();
		if (audioPlayer == null) {
			Debug.Log("select the audio player!");
			return;
		}
		List<AudioPlayerClipCollectionData> setup = new List<AudioPlayerClipCollectionData>(audioPlayer._setup);
		setup.Sort(delegate(AudioPlayerClipCollectionData xx, AudioPlayerClipCollectionData yy) {
			if (xx._type > yy._type) { return 1; }
			if (xx._type < yy._type) { return -1; }
			return 0;
		});
		//audioPlayer._setup = new AudioPlayerClipCollectionData[setup.Count];
		//setup.CopyTo(audioPlayer._setup);
		audioPlayer._setup = setup.ToArray();
		Debug.Log("sorted " + setup.Count + " entries.");
	}
	
    [MenuItem ("Utilities/AudioPlayer/RemoveDupes")]
    static void  RemoveDupes() {
		GameObject obj = Selection.activeObject as GameObject;
		AudioPlayer audioPlayer = obj.GetComponent<AudioPlayer>();
		if (audioPlayer == null) {
			Debug.Log("select the audio player!");
			return;
		}
		Dictionary<AudioPlayer.SoundType, AudioPlayerClipCollectionData> setup = new Dictionary<AudioPlayer.SoundType, AudioPlayerClipCollectionData>();
		foreach (var audio in audioPlayer._setup) {
			if (setup.ContainsKey(audio._type) == false && audio._type != AudioPlayer.SoundType.None) {
				setup.Add(audio._type, audio);
			}
		}
		//audioPlayer._setup = new AudioPlayerClipCollectionData[setup.Count];
		List<AudioPlayerClipCollectionData> newSetup = new List<AudioPlayerClipCollectionData>(setup.Values);
		//newSetup.CopyTo(audioPlayer._setup);
		audioPlayer._setup = newSetup.ToArray();
		Debug.Log("removed dupes.");
	}

}
#endif