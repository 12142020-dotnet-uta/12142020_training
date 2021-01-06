using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ModelLayer;
using ModelLayer.ViewModels;

namespace BusinessLogicLayer
{
	public class MapperClass
	{

		public PlayerViewModel ConvertPlayerToPlayerViewModel(Player player)
		{
			PlayerViewModel playerViewModel = new PlayerViewModel()
			{
				playerId = player.playerId,
				Fname = player.Fname,
				Lname = player.Lname,
				numLosses = player.numLosses,
				numWins = player.numWins,
				JpgStringImage = ConvertByteArrayToJpgString(player.ByteArrayImage)
			};

			return playerViewModel;
		}

		private string ConvertByteArrayToJpgString(byte[] byteArray)
		{
			if (byteArray != null)
			{
				string imageBase64Data = Convert.ToBase64String(byteArray, 0, byteArray.Length);
				string imageDataURL = string.Format($"data:image/jpg;base64,{imageBase64Data}");
				return imageDataURL;
			}
			else return null;
		}

		public byte[] ConvertIformFileToByteArray(IFormFile iformFile)
		{
			using (var ms = new MemoryStream())
			{
				// convert the IFormFile into a byte[]
				iformFile.CopyTo(ms);

				if (ms.Length > 2097152)// if it's bigger that 2 MB
				{
					return null;
				}
				else
				{
					byte[] a = ms.ToArray(); // put the string into the Image property
					return a;
				}
			}
		}
	}
}
