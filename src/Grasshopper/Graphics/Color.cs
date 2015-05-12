// Code borrowed from SharpDX library. License as follows:

/*
Copyright (c) 2010-2014 SharpDX - Alexandre Mutel

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using System;
using System.Runtime.InteropServices;

namespace Grasshopper.Graphics
{
	[StructLayout(LayoutKind.Sequential, Size = 4)]
	public struct Color : IEquatable<Color>, IFormattable
	{
		/// <summary>
		///     Zero color.
		/// </summary>
		public static readonly Color Zero = FromBgra(0);

		/// <summary>
		///     Transparent color.
		/// </summary>
		public static readonly Color Transparent = FromBgra(0);

		/// <summary>
		///     AliceBlue color.
		/// </summary>
		public static readonly Color AliceBlue = FromBgra(4293982463U);

		/// <summary>
		///     AntiqueWhite color.
		/// </summary>
		public static readonly Color AntiqueWhite = FromBgra(4294634455U);

		/// <summary>
		///     Aqua color.
		/// </summary>
		public static readonly Color Aqua = FromBgra(4278255615U);

		/// <summary>
		///     Aquamarine color.
		/// </summary>
		public static readonly Color Aquamarine = FromBgra(4286578644U);

		/// <summary>
		///     Azure color.
		/// </summary>
		public static readonly Color Azure = FromBgra(4293984255U);

		/// <summary>
		///     Beige color.
		/// </summary>
		public static readonly Color Beige = FromBgra(4294309340U);

		/// <summary>
		///     Bisque color.
		/// </summary>
		public static readonly Color Bisque = FromBgra(4294960324U);

		/// <summary>
		///     Black color.
		/// </summary>
		public static readonly Color Black = FromBgra(4278190080U);

		/// <summary>
		///     BlanchedAlmond color.
		/// </summary>
		public static readonly Color BlanchedAlmond = FromBgra(4294962125U);

		/// <summary>
		///     Blue color.
		/// </summary>
		public static readonly Color Blue = FromBgra(4278190335U);

		/// <summary>
		///     BlueViolet color.
		/// </summary>
		public static readonly Color BlueViolet = FromBgra(4287245282U);

		/// <summary>
		///     Brown color.
		/// </summary>
		public static readonly Color Brown = FromBgra(4289014314U);

		/// <summary>
		///     BurlyWood color.
		/// </summary>
		public static readonly Color BurlyWood = FromBgra(4292786311U);

		/// <summary>
		///     CadetBlue color.
		/// </summary>
		public static readonly Color CadetBlue = FromBgra(4284456608U);

		/// <summary>
		///     Chartreuse color.
		/// </summary>
		public static readonly Color Chartreuse = FromBgra(4286578432U);

		/// <summary>
		///     Chocolate color.
		/// </summary>
		public static readonly Color Chocolate = FromBgra(4291979550U);

		/// <summary>
		///     Coral color.
		/// </summary>
		public static readonly Color Coral = FromBgra(4294934352U);

		/// <summary>
		///     CornflowerBlue color.
		/// </summary>
		public static readonly Color CornflowerBlue = FromBgra(4284782061U);

		/// <summary>
		///     Cornsilk color.
		/// </summary>
		public static readonly Color Cornsilk = FromBgra(4294965468U);

		/// <summary>
		///     Crimson color.
		/// </summary>
		public static readonly Color Crimson = FromBgra(4292613180U);

		/// <summary>
		///     Cyan color.
		/// </summary>
		public static readonly Color Cyan = FromBgra(4278255615U);

		/// <summary>
		///     DarkBlue color.
		/// </summary>
		public static readonly Color DarkBlue = FromBgra(4278190219U);

		/// <summary>
		///     DarkCyan color.
		/// </summary>
		public static readonly Color DarkCyan = FromBgra(4278225803U);

		/// <summary>
		///     DarkGoldenrod color.
		/// </summary>
		public static readonly Color DarkGoldenrod = FromBgra(4290283019U);

		/// <summary>
		///     DarkGray color.
		/// </summary>
		public static readonly Color DarkGray = FromBgra(4289309097U);

		/// <summary>
		///     DarkGreen color.
		/// </summary>
		public static readonly Color DarkGreen = FromBgra(4278215680U);

		/// <summary>
		///     DarkKhaki color.
		/// </summary>
		public static readonly Color DarkKhaki = FromBgra(4290623339U);

		/// <summary>
		///     DarkMagenta color.
		/// </summary>
		public static readonly Color DarkMagenta = FromBgra(4287299723U);

		/// <summary>
		///     DarkOliveGreen color.
		/// </summary>
		public static readonly Color DarkOliveGreen = FromBgra(4283788079U);

		/// <summary>
		///     DarkOrange color.
		/// </summary>
		public static readonly Color DarkOrange = FromBgra(4294937600U);

		/// <summary>
		///     DarkOrchid color.
		/// </summary>
		public static readonly Color DarkOrchid = FromBgra(4288230092U);

		/// <summary>
		///     DarkRed color.
		/// </summary>
		public static readonly Color DarkRed = FromBgra(4287299584U);

		/// <summary>
		///     DarkSalmon color.
		/// </summary>
		public static readonly Color DarkSalmon = FromBgra(4293498490U);

		/// <summary>
		///     DarkSeaGreen color.
		/// </summary>
		public static readonly Color DarkSeaGreen = FromBgra(4287609995U);

		/// <summary>
		///     DarkSlateBlue color.
		/// </summary>
		public static readonly Color DarkSlateBlue = FromBgra(4282924427U);

		/// <summary>
		///     DarkSlateGray color.
		/// </summary>
		public static readonly Color DarkSlateGray = FromBgra(4281290575U);

		/// <summary>
		///     DarkTurquoise color.
		/// </summary>
		public static readonly Color DarkTurquoise = FromBgra(4278243025U);

		/// <summary>
		///     DarkViolet color.
		/// </summary>
		public static readonly Color DarkViolet = FromBgra(4287889619U);

		/// <summary>
		///     DeepPink color.
		/// </summary>
		public static readonly Color DeepPink = FromBgra(4294907027U);

		/// <summary>
		///     DeepSkyBlue color.
		/// </summary>
		public static readonly Color DeepSkyBlue = FromBgra(4278239231U);

		/// <summary>
		///     DimGray color.
		/// </summary>
		public static readonly Color DimGray = FromBgra(4285098345U);

		/// <summary>
		///     DodgerBlue color.
		/// </summary>
		public static readonly Color DodgerBlue = FromBgra(4280193279U);

		/// <summary>
		///     Firebrick color.
		/// </summary>
		public static readonly Color Firebrick = FromBgra(4289864226U);

		/// <summary>
		///     FloralWhite color.
		/// </summary>
		public static readonly Color FloralWhite = FromBgra(4294966000U);

		/// <summary>
		///     ForestGreen color.
		/// </summary>
		public static readonly Color ForestGreen = FromBgra(4280453922U);

		/// <summary>
		///     Fuchsia color.
		/// </summary>
		public static readonly Color Fuchsia = FromBgra(4294902015U);

		/// <summary>
		///     Gainsboro color.
		/// </summary>
		public static readonly Color Gainsboro = FromBgra(4292664540U);

		/// <summary>
		///     GhostWhite color.
		/// </summary>
		public static readonly Color GhostWhite = FromBgra(4294506751U);

		/// <summary>
		///     Gold color.
		/// </summary>
		public static readonly Color Gold = FromBgra(4294956800U);

		/// <summary>
		///     Goldenrod color.
		/// </summary>
		public static readonly Color Goldenrod = FromBgra(4292519200U);

		/// <summary>
		///     Gray color.
		/// </summary>
		public static readonly Color Gray = FromBgra(4286611584U);

		/// <summary>
		///     Green color.
		/// </summary>
		public static readonly Color Green = FromBgra(4278222848U);

		/// <summary>
		///     GreenYellow color.
		/// </summary>
		public static readonly Color GreenYellow = FromBgra(4289593135U);

		/// <summary>
		///     Honeydew color.
		/// </summary>
		public static readonly Color Honeydew = FromBgra(4293984240U);

		/// <summary>
		///     HotPink color.
		/// </summary>
		public static readonly Color HotPink = FromBgra(4294928820U);

		/// <summary>
		///     IndianRed color.
		/// </summary>
		public static readonly Color IndianRed = FromBgra(4291648604U);

		/// <summary>
		///     Indigo color.
		/// </summary>
		public static readonly Color Indigo = FromBgra(4283105410U);

		/// <summary>
		///     Ivory color.
		/// </summary>
		public static readonly Color Ivory = FromBgra(4294967280U);

		/// <summary>
		///     Khaki color.
		/// </summary>
		public static readonly Color Khaki = FromBgra(4293977740U);

		/// <summary>
		///     Lavender color.
		/// </summary>
		public static readonly Color Lavender = FromBgra(4293322490U);

		/// <summary>
		///     LavenderBlush color.
		/// </summary>
		public static readonly Color LavenderBlush = FromBgra(4294963445U);

		/// <summary>
		///     LawnGreen color.
		/// </summary>
		public static readonly Color LawnGreen = FromBgra(4286381056U);

		/// <summary>
		///     LemonChiffon color.
		/// </summary>
		public static readonly Color LemonChiffon = FromBgra(4294965965U);

		/// <summary>
		///     LightBlue color.
		/// </summary>
		public static readonly Color LightBlue = FromBgra(4289583334U);

		/// <summary>
		///     LightCoral color.
		/// </summary>
		public static readonly Color LightCoral = FromBgra(4293951616U);

		/// <summary>
		///     LightCyan color.
		/// </summary>
		public static readonly Color LightCyan = FromBgra(4292935679U);

		/// <summary>
		///     LightGoldenrodYellow color.
		/// </summary>
		public static readonly Color LightGoldenrodYellow = FromBgra(4294638290U);

		/// <summary>
		///     LightGray color.
		/// </summary>
		public static readonly Color LightGray = FromBgra(4292072403U);

		/// <summary>
		///     LightGreen color.
		/// </summary>
		public static readonly Color LightGreen = FromBgra(4287688336U);

		/// <summary>
		///     LightPink color.
		/// </summary>
		public static readonly Color LightPink = FromBgra(4294948545U);

		/// <summary>
		///     LightSalmon color.
		/// </summary>
		public static readonly Color LightSalmon = FromBgra(4294942842U);

		/// <summary>
		///     LightSeaGreen color.
		/// </summary>
		public static readonly Color LightSeaGreen = FromBgra(4280332970U);

		/// <summary>
		///     LightSkyBlue color.
		/// </summary>
		public static readonly Color LightSkyBlue = FromBgra(4287090426U);

		/// <summary>
		///     LightSlateGray color.
		/// </summary>
		public static readonly Color LightSlateGray = FromBgra(4286023833U);

		/// <summary>
		///     LightSteelBlue color.
		/// </summary>
		public static readonly Color LightSteelBlue = FromBgra(4289774814U);

		/// <summary>
		///     LightYellow color.
		/// </summary>
		public static readonly Color LightYellow = FromBgra(4294967264U);

		/// <summary>
		///     Lime color.
		/// </summary>
		public static readonly Color Lime = FromBgra(4278255360U);

		/// <summary>
		///     LimeGreen color.
		/// </summary>
		public static readonly Color LimeGreen = FromBgra(4281519410U);

		/// <summary>
		///     Linen color.
		/// </summary>
		public static readonly Color Linen = FromBgra(4294635750U);

		/// <summary>
		///     Magenta color.
		/// </summary>
		public static readonly Color Magenta = FromBgra(4294902015U);

		/// <summary>
		///     Maroon color.
		/// </summary>
		public static readonly Color Maroon = FromBgra(4286578688U);

		/// <summary>
		///     MediumAquamarine color.
		/// </summary>
		public static readonly Color MediumAquamarine = FromBgra(4284927402U);

		/// <summary>
		///     MediumBlue color.
		/// </summary>
		public static readonly Color MediumBlue = FromBgra(4278190285U);

		/// <summary>
		///     MediumOrchid color.
		/// </summary>
		public static readonly Color MediumOrchid = FromBgra(4290401747U);

		/// <summary>
		///     MediumPurple color.
		/// </summary>
		public static readonly Color MediumPurple = FromBgra(4287852763U);

		/// <summary>
		///     MediumSeaGreen color.
		/// </summary>
		public static readonly Color MediumSeaGreen = FromBgra(4282168177U);

		/// <summary>
		///     MediumSlateBlue color.
		/// </summary>
		public static readonly Color MediumSlateBlue = FromBgra(4286277870U);

		/// <summary>
		///     MediumSpringGreen color.
		/// </summary>
		public static readonly Color MediumSpringGreen = FromBgra(4278254234U);

		/// <summary>
		///     MediumTurquoise color.
		/// </summary>
		public static readonly Color MediumTurquoise = FromBgra(4282962380U);

		/// <summary>
		///     MediumVioletRed color.
		/// </summary>
		public static readonly Color MediumVioletRed = FromBgra(4291237253U);

		/// <summary>
		///     MidnightBlue color.
		/// </summary>
		public static readonly Color MidnightBlue = FromBgra(4279834992U);

		/// <summary>
		///     MintCream color.
		/// </summary>
		public static readonly Color MintCream = FromBgra(4294311930U);

		/// <summary>
		///     MistyRose color.
		/// </summary>
		public static readonly Color MistyRose = FromBgra(4294960353U);

		/// <summary>
		///     Moccasin color.
		/// </summary>
		public static readonly Color Moccasin = FromBgra(4294960309U);

		/// <summary>
		///     NavajoWhite color.
		/// </summary>
		public static readonly Color NavajoWhite = FromBgra(4294958765U);

		/// <summary>
		///     Navy color.
		/// </summary>
		public static readonly Color Navy = FromBgra(4278190208U);

		/// <summary>
		///     OldLace color.
		/// </summary>
		public static readonly Color OldLace = FromBgra(4294833638U);

		/// <summary>
		///     Olive color.
		/// </summary>
		public static readonly Color Olive = FromBgra(4286611456U);

		/// <summary>
		///     OliveDrab color.
		/// </summary>
		public static readonly Color OliveDrab = FromBgra(4285238819U);

		/// <summary>
		///     Orange color.
		/// </summary>
		public static readonly Color Orange = FromBgra(4294944000U);

		/// <summary>
		///     OrangeRed color.
		/// </summary>
		public static readonly Color OrangeRed = FromBgra(4294919424U);

		/// <summary>
		///     Orchid color.
		/// </summary>
		public static readonly Color Orchid = FromBgra(4292505814U);

		/// <summary>
		///     PaleGoldenrod color.
		/// </summary>
		public static readonly Color PaleGoldenrod = FromBgra(4293847210U);

		/// <summary>
		///     PaleGreen color.
		/// </summary>
		public static readonly Color PaleGreen = FromBgra(4288215960U);

		/// <summary>
		///     PaleTurquoise color.
		/// </summary>
		public static readonly Color PaleTurquoise = FromBgra(4289720046U);

		/// <summary>
		///     PaleVioletRed color.
		/// </summary>
		public static readonly Color PaleVioletRed = FromBgra(4292571283U);

		/// <summary>
		///     PapayaWhip color.
		/// </summary>
		public static readonly Color PapayaWhip = FromBgra(4294963157U);

		/// <summary>
		///     PeachPuff color.
		/// </summary>
		public static readonly Color PeachPuff = FromBgra(4294957753U);

		/// <summary>
		///     Peru color.
		/// </summary>
		public static readonly Color Peru = FromBgra(4291659071U);

		/// <summary>
		///     Pink color.
		/// </summary>
		public static readonly Color Pink = FromBgra(4294951115U);

		/// <summary>
		///     Plum color.
		/// </summary>
		public static readonly Color Plum = FromBgra(4292714717U);

		/// <summary>
		///     PowderBlue color.
		/// </summary>
		public static readonly Color PowderBlue = FromBgra(4289781990U);

		/// <summary>
		///     Purple color.
		/// </summary>
		public static readonly Color Purple = FromBgra(4286578816U);

		/// <summary>
		///     Red color.
		/// </summary>
		public static readonly Color Red = FromBgra(4294901760U);

		/// <summary>
		///     RosyBrown color.
		/// </summary>
		public static readonly Color RosyBrown = FromBgra(4290547599U);

		/// <summary>
		///     RoyalBlue color.
		/// </summary>
		public static readonly Color RoyalBlue = FromBgra(4282477025U);

		/// <summary>
		///     SaddleBrown color.
		/// </summary>
		public static readonly Color SaddleBrown = FromBgra(4287317267U);

		/// <summary>
		///     Salmon color.
		/// </summary>
		public static readonly Color Salmon = FromBgra(4294606962U);

		/// <summary>
		///     SandyBrown color.
		/// </summary>
		public static readonly Color SandyBrown = FromBgra(4294222944U);

		/// <summary>
		///     SeaGreen color.
		/// </summary>
		public static readonly Color SeaGreen = FromBgra(4281240407U);

		/// <summary>
		///     SeaShell color.
		/// </summary>
		public static readonly Color SeaShell = FromBgra(4294964718U);

		/// <summary>
		///     Sienna color.
		/// </summary>
		public static readonly Color Sienna = FromBgra(4288696877U);

		/// <summary>
		///     Silver color.
		/// </summary>
		public static readonly Color Silver = FromBgra(4290822336U);

		/// <summary>
		///     SkyBlue color.
		/// </summary>
		public static readonly Color SkyBlue = FromBgra(4287090411U);

		/// <summary>
		///     SlateBlue color.
		/// </summary>
		public static readonly Color SlateBlue = FromBgra(4285160141U);

		/// <summary>
		///     SlateGray color.
		/// </summary>
		public static readonly Color SlateGray = FromBgra(4285563024U);

		/// <summary>
		///     Snow color.
		/// </summary>
		public static readonly Color Snow = FromBgra(4294966010U);

		/// <summary>
		///     SpringGreen color.
		/// </summary>
		public static readonly Color SpringGreen = FromBgra(4278255487U);

		/// <summary>
		///     SteelBlue color.
		/// </summary>
		public static readonly Color SteelBlue = FromBgra(4282811060U);

		/// <summary>
		///     Tan color.
		/// </summary>
		public static readonly Color Tan = FromBgra(4291998860U);

		/// <summary>
		///     Teal color.
		/// </summary>
		public static readonly Color Teal = FromBgra(4278222976U);

		/// <summary>
		///     Thistle color.
		/// </summary>
		public static readonly Color Thistle = FromBgra(4292394968U);

		/// <summary>
		///     Tomato color.
		/// </summary>
		public static readonly Color Tomato = FromBgra(4294927175U);

		/// <summary>
		///     Turquoise color.
		/// </summary>
		public static readonly Color Turquoise = FromBgra(4282441936U);

		/// <summary>
		///     Violet color.
		/// </summary>
		public static readonly Color Violet = FromBgra(4293821166U);

		/// <summary>
		///     Wheat color.
		/// </summary>
		public static readonly Color Wheat = FromBgra(4294303411U);

		/// <summary>
		///     White color.
		/// </summary>
		public static readonly Color White = Color.FromBgra(uint.MaxValue);

		/// <summary>
		///     WhiteSmoke color.
		/// </summary>
		public static readonly Color WhiteSmoke = FromBgra(4294309365U);

		/// <summary>
		///     Yellow color.
		/// </summary>
		public static readonly Color Yellow = FromBgra(4294967040U);

		/// <summary>
		///     YellowGreen color.
		/// </summary>
		public static readonly Color YellowGreen = FromBgra(4288335154U);

		private const string toStringFormat = "A:{0} R:{1} G:{2} B:{3}";

		/// <summary>
		///     The red component of the color.
		/// </summary>
		public byte R;

		/// <summary>
		///     The green component of the color.
		/// </summary>
		public byte G;

		/// <summary>
		///     The blue component of the color.
		/// </summary>
		public byte B;

		/// <summary>
		///     The alpha component of the color.
		/// </summary>
		public byte A;

		/// <summary>
		///     Gets or sets the component at the specified index.
		/// </summary>
		/// <value>
		///     The value of the alpha, red, green, or blue component, depending on the index.
		/// </value>
		/// <param name="index">
		///     The index of the component to access. Use 0 for the alpha component, 1 for the red component, 2 for
		///     the green component, and 3 for the blue component.
		/// </param>
		/// <returns>
		///     The value of the component at the specified index.
		/// </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///     Thrown when the <paramref name="index" /> is out of the range
		///     [0, 3].
		/// </exception>
		public byte this[int index]
		{
			get
			{
				switch(index)
				{
					case 0:
						return R;
					case 1:
						return G;
					case 2:
						return B;
					case 3:
						return A;
					default:
						throw new ArgumentOutOfRangeException("index", "Indices for Color run from 0 to 3, inclusive.");
				}
			}
			set
			{
				switch(index)
				{
					case 0:
						R = value;
						break;
					case 1:
						G = value;
						break;
					case 2:
						B = value;
						break;
					case 3:
						A = value;
						break;
					default:
						throw new ArgumentOutOfRangeException("index", "Indices for Color run from 0 to 3, inclusive.");
				}
			}
		}

		static Color()
		{
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="T:SharpDX.Color" /> struct.
		/// </summary>
		/// <param name="value">The value that will be assigned to all components.</param>
		public Color(byte value)
		{
			A = R = G = B = value;
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="T:SharpDX.Color" /> struct.
		/// </summary>
		/// <param name="value">The value that will be assigned to all components.</param>
		public Color(float value)
		{
			A = R = G = B = ToByte(value);
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="T:SharpDX.Color" /> struct.
		/// </summary>
		/// <param name="red">The red component of the color.</param>
		/// <param name="green">The green component of the color.</param>
		/// <param name="blue">The blue component of the color.</param>
		/// <param name="alpha">The alpha component of the color.</param>
		public Color(byte red, byte green, byte blue, byte alpha)
		{
			R = red;
			G = green;
			B = blue;
			A = alpha;
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="T:SharpDX.Color" /> struct.  Alpha is set to 255.
		/// </summary>
		/// <param name="red">The red component of the color.</param>
		/// <param name="green">The green component of the color.</param>
		/// <param name="blue">The blue component of the color.</param>
		public Color(byte red, byte green, byte blue)
		{
			R = red;
			G = green;
			B = blue;
			A = byte.MaxValue;
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="T:SharpDX.Color" /> struct.  Passed values are clamped within byte
		///     range.
		/// </summary>
		/// <param name="red">The red component of the color.</param>
		/// <param name="green">The green component of the color.</param>
		/// <param name="blue">The blue component of the color.</param>
		public Color(int red, int green, int blue, int alpha)
		{
			R = ToByte(red);
			G = ToByte(green);
			B = ToByte(blue);
			A = ToByte(alpha);
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="T:SharpDX.Color" /> struct.  Alpha is set to 255.  Passed values are
		///     clamped within byte range.
		/// </summary>
		/// <param name="red">The red component of the color.</param>
		/// <param name="green">The green component of the color.</param>
		/// <param name="blue">The blue component of the color.</param>
		public Color(int red, int green, int blue)
		{
			this = new Color(red, green, blue, byte.MaxValue);
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="T:SharpDX.Color" /> struct.
		/// </summary>
		/// <param name="red">The red component of the color.</param>
		/// <param name="green">The green component of the color.</param>
		/// <param name="blue">The blue component of the color.</param>
		/// <param name="alpha">The alpha component of the color.</param>
		public Color(float red, float green, float blue, float alpha)
		{
			R = ToByte(red);
			G = ToByte(green);
			B = ToByte(blue);
			A = ToByte(alpha);
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="T:SharpDX.Color" /> struct.  Alpha is set to 255.
		/// </summary>
		/// <param name="red">The red component of the color.</param>
		/// <param name="green">The green component of the color.</param>
		/// <param name="blue">The blue component of the color.</param>
		public Color(float red, float green, float blue)
		{
			R = ToByte(red);
			G = ToByte(green);
			B = ToByte(blue);
			A = byte.MaxValue;
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="T:SharpDX.Color" /> struct.
		/// </summary>
		/// <param name="rgba">A packed integer containing all four color components in RGBA order.</param>
		public Color(uint rgba)
		{
			A = (byte)(rgba >> 24 & byte.MaxValue);
			B = (byte)(rgba >> 16 & byte.MaxValue);
			G = (byte)(rgba >> 8 & byte.MaxValue);
			R = (byte)(rgba & byte.MaxValue);
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="T:SharpDX.Color" /> struct.
		/// </summary>
		/// <param name="rgba">A packed integer containing all four color components in RGBA order.</param>
		public Color(int rgba)
		{
			A = (byte)(rgba >> 24 & byte.MaxValue);
			B = (byte)(rgba >> 16 & byte.MaxValue);
			G = (byte)(rgba >> 8 & byte.MaxValue);
			R = (byte)(rgba & byte.MaxValue);
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="T:SharpDX.Color" /> struct.
		/// </summary>
		/// <param name="values">
		///     The values to assign to the red, green, and blue, alpha components of the color. This must be an
		///     array with four elements.
		/// </param>
		/// <exception cref="T:System.ArgumentNullException">Thrown when <paramref name="values" /> is <c>null</c>.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///     Thrown when <paramref name="values" /> contains more or less
		///     than four elements.
		/// </exception>
		public Color(float[] values)
		{
			if(values == null)
				throw new ArgumentNullException("values");
			if(values.Length != 4)
				throw new ArgumentOutOfRangeException("values", "There must be four and only four input values for Color.");
			R = ToByte(values[0]);
			G = ToByte(values[1]);
			B = ToByte(values[2]);
			A = ToByte(values[3]);
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="T:SharpDX.Color" /> struct.
		/// </summary>
		/// <param name="values">
		///     The values to assign to the alpha, red, green, and blue components of the color. This must be an
		///     array with four elements.
		/// </param>
		/// <exception cref="T:System.ArgumentNullException">Thrown when <paramref name="values" /> is <c>null</c>.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///     Thrown when <paramref name="values" /> contains more or less
		///     than four elements.
		/// </exception>
		public Color(byte[] values)
		{
			if(values == null)
				throw new ArgumentNullException("values");
			if(values.Length != 4)
				throw new ArgumentOutOfRangeException("values", "There must be four and only four input values for Color.");
			R = values[0];
			G = values[1];
			B = values[2];
			A = values[3];
		}


		/// <summary>
		///     Performs an explicit conversion from <see cref="T:System.Int32" /> to <see cref="T:SharpDX.Color" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		///     The result of the conversion.
		/// </returns>
		public static explicit operator int(Color value)
		{
			return value.ToRgba();
		}

		/// <summary>
		///     Performs an explicit conversion from <see cref="T:System.Int32" /> to <see cref="T:SharpDX.Color" />.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		///     The result of the conversion.
		/// </returns>
		public static explicit operator Color(int value)
		{
			return new Color(value);
		}

		/// <summary>
		///     Adds two colors.
		/// </summary>
		/// <param name="left">The first color to add.</param>
		/// <param name="right">The second color to add.</param>
		/// <returns>
		///     The sum of the two colors.
		/// </returns>
		public static Color operator +(Color left, Color right)
		{
			return new Color(left.R + right.R, left.G + right.G, left.B + right.B, left.A + right.A);
		}

		/// <summary>
		///     Assert a color (return it unchanged).
		/// </summary>
		/// <param name="value">The color to assert (unchanged).</param>
		/// <returns>
		///     The asserted (unchanged) color.
		/// </returns>
		public static Color operator +(Color value)
		{
			return value;
		}

		/// <summary>
		///     Subtracts two colors.
		/// </summary>
		/// <param name="left">The first color to subtract.</param>
		/// <param name="right">The second color to subtract.</param>
		/// <returns>
		///     The difference of the two colors.
		/// </returns>
		public static Color operator -(Color left, Color right)
		{
			return new Color(left.R - right.R, left.G - right.G, left.B - right.B, left.A - right.A);
		}

		/// <summary>
		///     Negates a color.
		/// </summary>
		/// <param name="value">The color to negate.</param>
		/// <returns>
		///     A negated color.
		/// </returns>
		public static Color operator -(Color value)
		{
			return new Color(-value.R, -value.G, -value.B, -value.A);
		}

		/// <summary>
		///     Scales a color.
		/// </summary>
		/// <param name="scale">The factor by which to scale the color.</param>
		/// <param name="value">The color to scale.</param>
		/// <returns>
		///     The scaled color.
		/// </returns>
		public static Color operator *(float scale, Color value)
		{
			return new Color((byte)(value.R*(double)scale), (byte)(value.G*(double)scale), (byte)(value.B*(double)scale), (byte)(value.A*(double)scale));
		}

		/// <summary>
		///     Scales a color.
		/// </summary>
		/// <param name="value">The factor by which to scale the color.</param>
		/// <param name="scale">The color to scale.</param>
		/// <returns>
		///     The scaled color.
		/// </returns>
		public static Color operator *(Color value, float scale)
		{
			return new Color((byte)(value.R*(double)scale), (byte)(value.G*(double)scale), (byte)(value.B*(double)scale), (byte)(value.A*(double)scale));
		}

		/// <summary>
		///     Modulates two colors.
		/// </summary>
		/// <param name="left">The first color to modulate.</param>
		/// <param name="right">The second color to modulate.</param>
		/// <returns>
		///     The modulated color.
		/// </returns>
		public static Color operator *(Color left, Color right)
		{
			return new Color((byte)(left.R*right.R/(double)byte.MaxValue), (byte)(left.G*right.G/(double)byte.MaxValue), (byte)(left.B*right.B/(double)byte.MaxValue), (byte)(left.A*right.A/(double)byte.MaxValue));
		}

		/// <summary>
		///     Tests for equality between two objects.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///     <c>true</c> if <paramref name="left" /> has the same value as <paramref name="right" />; otherwise, <c>false</c>.
		/// </returns>
		public static bool operator ==(Color left, Color right)
		{
			return left.Equals(right);
		}

		/// <summary>
		///     Tests for inequality between two objects.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///     <c>true</c> if <paramref name="left" /> has a different value than <paramref name="right" />; otherwise,
		///     <c>false</c>.
		/// </returns>
		public static bool operator !=(Color left, Color right)
		{
			return !left.Equals(right);
		}

		/// <summary>
		///     Converts the color into a packed integer.
		/// </summary>
		/// <returns>
		///     A packed integer containing all four color components.
		/// </returns>
		public int ToBgra()
		{
			return B | G << 8 | R << 16 | A << 24;
		}

		/// <summary>
		///     Converts the color into a packed integer.
		/// </summary>
		/// <returns>
		///     A packed integer containing all four color components.
		/// </returns>
		public int ToRgba()
		{
			return R | G << 8 | B << 16 | A << 24;
		}

		/// <summary>
		///     Converts the color into a packed integer.
		/// </summary>
		/// <returns>
		///     A packed integer containing all four color components.
		/// </returns>
		public int ToAbgr()
		{
			return A | B << 8 | G << 16 | R << 24;
		}

		/// <summary>
		///     Creates an array containing the elements of the color.
		/// </summary>
		/// <returns>
		///     A four-element array containing the components of the color in RGBA order.
		/// </returns>
		public byte[] ToArray()
		{
			return new byte[4]
			{
				R,
				G,
				B,
				A
			};
		}

		/// <summary>
		///     Gets the brightness.
		/// </summary>
		/// <returns>
		///     The Hue-Saturation-Brightness (HSB) saturation for this <see cref="T:SharpDX.Color" />
		/// </returns>
		public float GetBrightness()
		{
			float num1 = R/(float)byte.MaxValue;
			float num2 = G/(float)byte.MaxValue;
			float num3 = B/(float)byte.MaxValue;
			float num4 = num1;
			float num5 = num1;
			if(num2 > (double)num4)
				num4 = num2;
			if(num3 > (double)num4)
				num4 = num3;
			if(num2 < (double)num5)
				num5 = num2;
			if(num3 < (double)num5)
				num5 = num3;
			return (float)((num4 + (double)num5)/2.0);
		}

		/// <summary>
		///     Gets the hue.
		/// </summary>
		/// <returns>
		///     The Hue-Saturation-Brightness (HSB) saturation for this <see cref="T:SharpDX.Color" />
		/// </returns>
		public float GetHue()
		{
			if(R == G && G == B)
				return 0.0f;
			float num1 = R/(float)byte.MaxValue;
			float num2 = G/(float)byte.MaxValue;
			float num3 = B/(float)byte.MaxValue;
			float num4 = 0.0f;
			float num5 = num1;
			float num6 = num1;
			if(num2 > (double)num5)
				num5 = num2;
			if(num3 > (double)num5)
				num5 = num3;
			if(num2 < (double)num6)
				num6 = num2;
			if(num3 < (double)num6)
				num6 = num3;
			float num7 = num5 - num6;
			if(num1 == (double)num5)
				num4 = (num2 - num3)/num7;
			else if(num2 == (double)num5)
				num4 = (float)(2.0 + (num3 - (double)num1)/num7);
			else if(num3 == (double)num5)
				num4 = (float)(4.0 + (num1 - (double)num2)/num7);
			float num8 = num4*60f;
			if(num8 < 0.0)
				num8 += 360f;
			return num8;
		}

		/// <summary>
		///     Gets the saturation.
		/// </summary>
		/// <returns>
		///     The Hue-Saturation-Brightness (HSB) saturation for this <see cref="T:SharpDX.Color" />
		/// </returns>
		public float GetSaturation()
		{
			float num1 = R/(float)byte.MaxValue;
			float num2 = G/(float)byte.MaxValue;
			float num3 = B/(float)byte.MaxValue;
			float num4 = 0.0f;
			float num5 = num1;
			float num6 = num1;
			if(num2 > (double)num5)
				num5 = num2;
			if(num3 > (double)num5)
				num5 = num3;
			if(num2 < (double)num6)
				num6 = num2;
			if(num3 < (double)num6)
				num6 = num3;
			if(num5 != (double)num6)
				num4 = ((double)num5 + (double)num6)/2.0 > 0.5 ? (float)((num5 - (double)num6)/(2.0 - num5 - num6)) : (float)((num5 - (double)num6)/(num5 + (double)num6));
			return num4;
		}

		/// <summary>
		///     Adds two colors.
		/// </summary>
		/// <param name="left">The first color to add.</param>
		/// <param name="right">The second color to add.</param>
		/// <param name="result">When the method completes, completes the sum of the two colors.</param>
		public static void Add(ref Color left, ref Color right, out Color result)
		{
			result.A = (byte)(left.A + (uint)right.A);
			result.R = (byte)(left.R + (uint)right.R);
			result.G = (byte)(left.G + (uint)right.G);
			result.B = (byte)(left.B + (uint)right.B);
		}

		/// <summary>
		///     Adds two colors.
		/// </summary>
		/// <param name="left">The first color to add.</param>
		/// <param name="right">The second color to add.</param>
		/// <returns>
		///     The sum of the two colors.
		/// </returns>
		public static Color Add(Color left, Color right)
		{
			return new Color(left.R + right.R, left.G + right.G, left.B + right.B, left.A + right.A);
		}

		/// <summary>
		///     Subtracts two colors.
		/// </summary>
		/// <param name="left">The first color to subtract.</param>
		/// <param name="right">The second color to subtract.</param>
		/// <param name="result">WHen the method completes, contains the difference of the two colors.</param>
		public static void Subtract(ref Color left, ref Color right, out Color result)
		{
			result.A = (byte)(left.A - (uint)right.A);
			result.R = (byte)(left.R - (uint)right.R);
			result.G = (byte)(left.G - (uint)right.G);
			result.B = (byte)(left.B - (uint)right.B);
		}

		/// <summary>
		///     Subtracts two colors.
		/// </summary>
		/// <param name="left">The first color to subtract.</param>
		/// <param name="right">The second color to subtract</param>
		/// <returns>
		///     The difference of the two colors.
		/// </returns>
		public static Color Subtract(Color left, Color right)
		{
			return new Color(left.R - right.R, left.G - right.G, left.B - right.B, left.A - right.A);
		}

		/// <summary>
		///     Modulates two colors.
		/// </summary>
		/// <param name="left">The first color to modulate.</param>
		/// <param name="right">The second color to modulate.</param>
		/// <param name="result">When the method completes, contains the modulated color.</param>
		public static void Modulate(ref Color left, ref Color right, out Color result)
		{
			result.A = (byte)(left.A*right.A/(double)byte.MaxValue);
			result.R = (byte)(left.R*right.R/(double)byte.MaxValue);
			result.G = (byte)(left.G*right.G/(double)byte.MaxValue);
			result.B = (byte)(left.B*right.B/(double)byte.MaxValue);
		}

		/// <summary>
		///     Modulates two colors.
		/// </summary>
		/// <param name="left">The first color to modulate.</param>
		/// <param name="right">The second color to modulate.</param>
		/// <returns>
		///     The modulated color.
		/// </returns>
		public static Color Modulate(Color left, Color right)
		{
			return new Color(left.R*right.R, left.G*right.G, left.B*right.B, left.A*right.A);
		}

		/// <summary>
		///     Scales a color.
		/// </summary>
		/// <param name="value">The color to scale.</param>
		/// <param name="scale">The amount by which to scale.</param>
		/// <param name="result">When the method completes, contains the scaled color.</param>
		public static void Scale(ref Color value, float scale, out Color result)
		{
			result.A = (byte)(value.A*(double)scale);
			result.R = (byte)(value.R*(double)scale);
			result.G = (byte)(value.G*(double)scale);
			result.B = (byte)(value.B*(double)scale);
		}

		/// <summary>
		///     Scales a color.
		/// </summary>
		/// <param name="value">The color to scale.</param>
		/// <param name="scale">The amount by which to scale.</param>
		/// <returns>
		///     The scaled color.
		/// </returns>
		public static Color Scale(Color value, float scale)
		{
			return new Color((byte)(value.R*(double)scale), (byte)(value.G*(double)scale), (byte)(value.B*(double)scale), (byte)(value.A*(double)scale));
		}

		/// <summary>
		///     Negates a color.
		/// </summary>
		/// <param name="value">The color to negate.</param>
		/// <param name="result">When the method completes, contains the negated color.</param>
		public static void Negate(ref Color value, out Color result)
		{
			result.A = (byte)(byte.MaxValue - (uint)value.A);
			result.R = (byte)(byte.MaxValue - (uint)value.R);
			result.G = (byte)(byte.MaxValue - (uint)value.G);
			result.B = (byte)(byte.MaxValue - (uint)value.B);
		}

		/// <summary>
		///     Negates a color.
		/// </summary>
		/// <param name="value">The color to negate.</param>
		/// <returns>
		///     The negated color.
		/// </returns>
		public static Color Negate(Color value)
		{
			return new Color(byte.MaxValue - value.R, byte.MaxValue - value.G, byte.MaxValue - value.B, byte.MaxValue - value.A);
		}

		/// <summary>
		///     Restricts a value to be within a specified range.
		/// </summary>
		/// <param name="value">The value to clamp.</param>
		/// <param name="min">The minimum value.</param>
		/// <param name="max">The maximum value.</param>
		/// <param name="result">When the method completes, contains the clamped value.</param>
		public static void Clamp(ref Color value, ref Color min, ref Color max, out Color result)
		{
			byte num1 = value.A;
			byte num2 = (int)num1 > (int)max.A ? max.A : num1;
			byte alpha = (int)num2 < (int)min.A ? min.A : num2;
			byte num3 = value.R;
			byte num4 = (int)num3 > (int)max.R ? max.R : num3;
			byte red = (int)num4 < (int)min.R ? min.R : num4;
			byte num5 = value.G;
			byte num6 = (int)num5 > (int)max.G ? max.G : num5;
			byte green = (int)num6 < (int)min.G ? min.G : num6;
			byte num7 = value.B;
			byte num8 = (int)num7 > (int)max.B ? max.B : num7;
			byte blue = (int)num8 < (int)min.B ? min.B : num8;
			result = new Color(red, green, blue, alpha);
		}

		/// <summary>
		///     Computes the premultiplied value of the provided color.
		/// </summary>
		/// <param name="value">The non-premultiplied value.</param>
		/// <param name="result">The premultiplied result.</param>
		public static void Premultiply(ref Color value, out Color result)
		{
			float num = value.A/65025f;
			result.A = value.A;
			result.R = ToByte(value.R*num);
			result.G = ToByte(value.G*num);
			result.B = ToByte(value.B*num);
		}

		/// <summary>
		///     Computes the premultiplied value of the provided color.
		/// </summary>
		/// <param name="value">The non-premultiplied value.</param>
		/// <returns>
		///     The premultiplied result.
		/// </returns>
		public static Color Premultiply(Color value)
		{
			Color result;
			Premultiply(ref value, out result);
			return result;
		}

		/// <summary>
		///     Converts the color from a packed BGRA integer.
		/// </summary>
		/// <param name="color">A packed integer containing all four color components in BGRA order</param>
		/// <returns>
		///     A color.
		/// </returns>
		public static Color FromBgra(int color)
		{
			return new Color((byte)(color >> 16 & byte.MaxValue), (byte)(color >> 8 & byte.MaxValue), (byte)(color & byte.MaxValue), (byte)(color >> 24 & byte.MaxValue));
		}

		/// <summary>
		///     Converts the color from a packed BGRA integer.
		/// </summary>
		/// <param name="color">A packed integer containing all four color components in BGRA order</param>
		/// <returns>
		///     A color.
		/// </returns>
		public static Color FromBgra(uint color)
		{
			return FromBgra((int)color);
		}

		/// <summary>
		///     Converts the color from a packed ABGR integer.
		/// </summary>
		/// <param name="color">A packed integer containing all four color components in ABGR order</param>
		/// <returns>
		///     A color.
		/// </returns>
		public static Color FromAbgr(int color)
		{
			return new Color((byte)(color >> 24), (byte)(color >> 16), (byte)(color >> 8), (byte)color);
		}

		/// <summary>
		///     Converts the color from a packed ABGR integer.
		/// </summary>
		/// <param name="color">A packed integer containing all four color components in ABGR order</param>
		/// <returns>
		///     A color.
		/// </returns>
		public static Color FromAbgr(uint color)
		{
			return FromAbgr((int)color);
		}

		/// <summary>
		///     Converts the color from a packed BGRA integer.
		/// </summary>
		/// <param name="color">A packed integer containing all four color components in RGBA order</param>
		/// <returns>
		///     A color.
		/// </returns>
		public static Color FromRgba(int color)
		{
			return new Color(color);
		}

		/// <summary>
		///     Converts the color from a packed BGRA integer.
		/// </summary>
		/// <param name="color">A packed integer containing all four color components in RGBA order</param>
		/// <returns>
		///     A color.
		/// </returns>
		public static Color FromRgba(uint color)
		{
			return new Color(color);
		}

		/// <summary>
		///     Returns a <see cref="T:System.String" /> that represents this instance.
		/// </summary>
		/// <param name="formatProvider">The format provider.</param>
		/// <returns>
		///     A <see cref="T:System.String" /> that represents this instance.
		/// </returns>
		public string ToString(IFormatProvider formatProvider)
		{
			return string.Format(formatProvider, "A:{0} R:{1} G:{2} B:{3}", (object)A, (object)R, (object)G, (object)B);
		}

		/// <summary>
		///     Returns a <see cref="T:System.String" /> that represents this instance.
		/// </summary>
		/// <param name="format">The format to apply to each channel element (byte).</param>
		/// <param name="formatProvider">The format provider.</param>
		/// <returns>
		///     A <see cref="T:System.String" /> that represents this instance.
		/// </returns>
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if(format == null)
				return ToString(formatProvider);
			return string.Format(formatProvider, "A:{0} R:{1} G:{2} B:{3}", (object)A.ToString(format, formatProvider), (object)R.ToString(format, formatProvider), (object)G.ToString(format, formatProvider), (object)B.ToString(format, formatProvider));
		}

		/// <summary>
		///     Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
		/// </returns>
		public override int GetHashCode()
		{
			return ((R.GetHashCode()*397 ^ G.GetHashCode())*397 ^ B.GetHashCode())*397 ^ A.GetHashCode();
		}

		/// <summary>
		///     Determines whether the specified <see cref="T:SharpDX.Color" /> is equal to this instance.
		/// </summary>
		/// <param name="other">The <see cref="T:SharpDX.Color" /> to compare with this instance.</param>
		/// <returns>
		///     <c>true</c> if the specified <see cref="T:SharpDX.Color" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public bool Equals(Color other)
		{
			if(R == other.R && G == other.G && B == other.B)
				return A == other.A;
			return false;
		}

		/// <summary>
		///     Determines whether the specified <see cref="T:System.Object" /> is equal to this instance.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Object" /> to compare with this instance.</param>
		/// <returns>
		///     <c>true</c> if the specified <see cref="T:System.Object" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object value)
		{
			if(value == null || !ReferenceEquals(value.GetType(), typeof(Color)))
				return false;
			return Equals((Color)value);
		}

		private static byte ToByte(float component)
		{
			return ToByte((int)(component*(double)byte.MaxValue));
		}

		public static byte ToByte(int value)
		{
			return value < 0 ? (byte)0 : (value > (int)byte.MaxValue ? byte.MaxValue : (byte)value);
		}
	}
}
