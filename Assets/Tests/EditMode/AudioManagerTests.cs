using NUnit.Framework;
using UnityEngine;

public class AudioManagerTests
{
    private GameObject audioManagerGO;
    private AudioManager audioManager;
    private AudioSource musicSource;
    private AudioSource sfxSource;

    [SetUp]
    public void SetUp()
    {
        // Skapa GameObject med två AudioSources
        audioManagerGO = new GameObject("AudioManager");
        musicSource = audioManagerGO.AddComponent<AudioSource>();
        sfxSource = audioManagerGO.AddComponent<AudioSource>();

        // Lägg till AudioManager
        audioManager = audioManagerGO.AddComponent<AudioManager>();

        // Använd reflection för att sätta [SerializeField] fälten
        var musicField = typeof(AudioManager).GetField("musicSource", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var sfxField = typeof(AudioManager).GetField("SFXSource", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        musicField.SetValue(audioManager, musicSource);
        sfxField.SetValue(audioManager, sfxSource);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(audioManagerGO);
    }

    [Test]
    public void MusicSource_IsSetToBackground_InTestMethod()
    {
        // Arrange
        var dummyClip = AudioClip.Create("dummy", 44100, 1, 44100, false);
        var backgroundField = typeof(AudioManager).GetField("background", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        backgroundField.SetValue(audioManager, dummyClip);

        // Act
        audioManager.SetBackgroundManually();

        // Assert
        Assert.AreEqual(dummyClip, musicSource.clip);
    }

    [Test]
    public void PlaySFX_DoesNotThrow_WithValidClip()
    {
        // Arrange
        var dummyClip = AudioClip.Create("testSFX", 44100, 1, 44100, false);

        // Act & Assert
        Assert.DoesNotThrow(() => audioManager.PlaySFX(dummyClip));
    }

    [Test]
    public void PlaySFX_DoesNotThrow_WithNullClip()
    {
        // Act & Assert
        Assert.DoesNotThrow(() => audioManager.PlaySFX(null));
    }
}
