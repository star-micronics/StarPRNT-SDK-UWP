using StarIO_Extension;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;

namespace StarPRNTSDK
{
    public class MelodySpeakerFunctions
    {
        public static byte[] CreatePlayingRegisteredSoundData(MelodySpeakerModel model, bool specifySound, MelodySpeakerSoundStorageArea soundStorageArea, int soundNumber, bool specifyVolume, int volume)
        {
            IMelodySpeakerCommandBuilder builder = StarIoExt.CreateMelodySpeakerCommandBuilder(model);

            SoundSetting setting = new SoundSetting();

            if (specifySound)
            {
                setting.SoundStorageArea = soundStorageArea;
                setting.SoundNumber = soundNumber;
            }

            if (specifyVolume)
            {
                setting.Volume = volume;
            }

            builder.AppendSound(setting);

            return builder.Commands;
        }

        public static byte[] CreatePlayingSoundData(MelodySpeakerModel model, StorageFile file, bool specifyVolume, int volume)
        {
            IAsyncOperation<IBuffer> readOperation = FileIO.ReadBufferAsync(file);

            byte[] data = readOperation.AsTask().Result.ToArray();

            IMelodySpeakerCommandBuilder builder = StarIoExt.CreateMelodySpeakerCommandBuilder(model);

            SoundSetting setting = new SoundSetting();

            if (specifyVolume)
            {
                setting.Volume = volume;
            }

            builder.AppendSoundData(data, setting);

            return builder.Commands;
        }
    }
}
