﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Xna.Framework.Graphics
{
	public class ComputeParameter
	{
		public string Name { get; private set; }
		public string TypeName { get; private set; }
		internal IntPtr variablePtr;
		internal ComputeShader parentEffect;

		public ComputeParameter(string name, string typeName, ComputeShader effect, IntPtr variablePtr)
		{
			Name = name;
			TypeName = typeName;
			this.parentEffect = effect;
			this.variablePtr = variablePtr;
		}

		public void SetRWBuffer(ComputeBuffer buffer)
		{
			FNA3D.FX11_Effect_Variable_SetUnorderedAccessView_ComputeBuffer(variablePtr, buffer == null ? IntPtr.Zero : buffer.buffer);
		}

		public void SetBuffer(ComputeBuffer buffer)
		{
			FNA3D.FX11_Effect_Variable_SetShaderResourceView_ComputeBuffer(variablePtr, buffer == null ? IntPtr.Zero : buffer.buffer);
		}

		public void SetRWTexture(Texture texture)
		{
			if (texture == null || texture.IsRandomAccess)
			{
				FNA3D.FX11_Effect_Variable_SetUnorderedAccessView_Texture(variablePtr, texture == null ? IntPtr.Zero : texture.texture);
				return;
			}
			throw new InvalidOperationException("SetRWTexture2D only accept textures that have random access enabled");
		}

		public void SetTexture(Texture texture)
		{
			FNA3D.FX11_Effect_Variable_SetShaderResourceView_Texture(variablePtr, texture.texture);
		}

		public void SetValue(int value)
		{
			FNA3D.FX11_Effect_Variable_SetInt(variablePtr, value);
		}

		public void SetValue(float value)
		{
			FNA3D.FX11_Effect_Variable_SetFloat(variablePtr, value);
		}

		public void SetValue(bool value)
		{
			FNA3D.FX11_Effect_Variable_SetBool(variablePtr, value);
		}

		public void SetValue(int[] value)
		{
			// Currently using this ugly approach, we will switch to directly write if new effect system is ready
			IntPtr intBuf = Marshal.AllocHGlobal(value.Length * Marshal.SizeOf<int>());
			unsafe
			{
				int* ptr = (int*)intBuf;
				for (int i = 0; i < value.Length; i++)
				{
					ptr[i] = value[i];
				}
			}
			FNA3D.FX11_Effect_Variable_SetIntArray(variablePtr, intBuf, 0, value.Length);
			Marshal.FreeHGlobal(intBuf);
		}

		public void SetValue(float[] value)
		{
			// Currently using this ugly approach, we will switch to directly write if new effect system is ready
			IntPtr floatBuf = Marshal.AllocHGlobal(value.Length * Marshal.SizeOf<float>());
			unsafe
			{
				float* ptr = (float*)floatBuf;
				for (int i = 0; i < value.Length; i++)
				{
					ptr[i] = value[i];
				}
			}
			FNA3D.FX11_Effect_Variable_SetFloatArray(variablePtr, floatBuf, 0, value.Length);
			Marshal.FreeHGlobal(floatBuf);
		}

		public void SetValue(Vector2 value)
		{
			// Currently using this ugly approach, we will switch to directly write if new effect system is ready
			IntPtr floatBuf = Marshal.AllocHGlobal(4 * Marshal.SizeOf<float>());
			unsafe
			{
				float* ptr = (float*)floatBuf;
				ptr[0] = value.X;
				ptr[1] = value.Y;
			}
			FNA3D.FX11_Effect_Variable_SetVectorf(variablePtr, floatBuf);
			Marshal.FreeHGlobal(floatBuf);
		}

		public void SetValue(Vector3 value)
		{
			// Currently using this ugly approach, we will switch to directly write if new effect system is ready
			IntPtr floatBuf = Marshal.AllocHGlobal(4 * Marshal.SizeOf<float>());
			unsafe
			{
				float* ptr = (float*)floatBuf;
				ptr[0] = value.X;
				ptr[1] = value.Y;
				ptr[2] = value.Z;
			}
			FNA3D.FX11_Effect_Variable_SetVectorf(variablePtr, floatBuf);
			Marshal.FreeHGlobal(floatBuf);
		}

		public void SetValue(Vector4 value)
		{
			// Currently using this ugly approach, we will switch to directly write if new effect system is ready
			IntPtr floatBuf = Marshal.AllocHGlobal(4 * Marshal.SizeOf<float>());
			unsafe
			{
				float* ptr = (float*)floatBuf;
				ptr[0] = value.X;
				ptr[1] = value.Y;
				ptr[2] = value.Z;
				ptr[3] = value.W;
			}
			FNA3D.FX11_Effect_Variable_SetVectorf(variablePtr, floatBuf);
			Marshal.FreeHGlobal(floatBuf);
		}

		public void SetValue(Matrix value)
		{
			IntPtr floatBuf = Marshal.AllocHGlobal(16 * Marshal.SizeOf<float>());
			// FIXME: All Matrix sizes... this will get ugly. -flibit
			unsafe
			{
				float* dstPtr = (float*)floatBuf;
				dstPtr[0] = value.M11;
				dstPtr[1] = value.M12;
				dstPtr[2] = value.M13;
				dstPtr[3] = value.M14;
				dstPtr[4] = value.M21;
				dstPtr[5] = value.M22;
				dstPtr[6] = value.M23;
				dstPtr[7] = value.M24;
				dstPtr[8] = value.M31;
				dstPtr[9] = value.M32;
				dstPtr[10] = value.M33;
				dstPtr[11] = value.M34;
				dstPtr[12] = value.M41;
				dstPtr[13] = value.M42;
				dstPtr[14] = value.M43;
				dstPtr[15] = value.M44;
			}
			FNA3D.FX11_Effect_Variable_SetMatrix4f(variablePtr, floatBuf);
			Marshal.FreeHGlobal(floatBuf);
		}
	}
}
