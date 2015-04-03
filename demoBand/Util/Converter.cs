using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace demoBand.Util
{
    public class Converter
    {

        public static async Task<byte[]> AudioStreamToByteArray(IRandomAccessStream audioStream)
        {
            var bytes = new Byte[0];
            var reader = new DataReader(audioStream.GetInputStreamAt(0));
            bytes = new Byte[audioStream.Size];
            await reader.LoadAsync((uint)audioStream.Size);
            reader.ReadBytes(bytes);
            return bytes;

        }



        public static async Task<IRandomAccessStream> arrayToAudioStream(byte[] array)
        {
            IRandomAccessStream audioStream = new InMemoryRandomAccessStream();
            using (DataWriter writer = new DataWriter(audioStream.GetOutputStreamAt(0)))
            {
                writer.WriteBytes(array);
                await writer.StoreAsync();
            }
            return audioStream;
        }


    }
}
