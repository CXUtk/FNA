using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Graphics
{
	public class ComputeShader : GraphicsResource
	{
		internal IntPtr glComputeShader;
		public ComputeParameterCollection Parameters
		{
			get;
			private set;
		}
		public ComputeTechniqueCollection Techniques
		{
			get;
			private set;
		}
		public ComputeShader(GraphicsDevice graphicsDevice, byte[] effectBinaryCode, uint effectCodeLength)
		{
			GraphicsDevice = graphicsDevice;

			FNA3D.FNA3D_CreateNewEffect(
				graphicsDevice.GLDevice,
				effectBinaryCode,
				effectCodeLength,
				out glComputeShader
			);

			parseData();
		}

		private unsafe void parseData()
		{
			FNA3D.FX11_Effect_CreateReflectionData(glComputeShader, out EffectDesc res);
			List<ComputeTechnique> techniques = new List<ComputeTechnique>((int)res.ShaderDesc.Techniques);
			var techs = (D3DX11_TECHNIQUE_DESC*)res.Techniques;
			var ptechs = (IntPtr*)res.PTechniques;
			for (int i = 0; i < res.ShaderDesc.Techniques; i++)
			{
				techniques.Add(CreateTechnique(i, techs[i], ptechs[i], res));
			}
			Techniques = new ComputeTechniqueCollection(techniques);

			List<ComputeParameter> parameters = new List<ComputeParameter>();
			var param = (D3DX11_EFFECT_VARIABLE_DESC*)res.Variables;
			var paramTypes = (D3DX11_EFFECT_TYPE_DESC*)res.VariableTypes;
			var pparam = (IntPtr*)res.PVariables;
			for(int i = 0; i < res.ShaderDesc.GlobalVariables; i++)
			{
				parameters.Add(new ComputeParameter(Marshal.PtrToStringAnsi(param[i].Name), Marshal.PtrToStringAnsi(paramTypes[i].TypeName), this, pparam[i]));
			}
			Parameters = new ComputeParameterCollection(parameters);

			FNA3D.FX11_Effect_ReleaseReflectionData(ref res);
		}

		private unsafe ComputeTechnique CreateTechnique(int i, in D3DX11_TECHNIQUE_DESC tech, IntPtr pTechnique, in EffectDesc desc)
		{
			var name = Marshal.PtrToStringAnsi(tech.Name);
			var technique = new ComputeTechnique(name, this, pTechnique);

			List<ComputePass> passes = new List<ComputePass>((int)tech.Passes);
			var pass = (D3DX11_PASS_DESC*)((IntPtr*)desc.Passes)[i];
			for (int j = 0; j < tech.Passes; j++)
			{
				passes.Add(new ComputePass(Marshal.PtrToStringAnsi(pass[j].Name), this, technique, ((IntPtr**)desc.PPasses)[i][j]));
			}
			technique.SetPasses(passes);
			return technique;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct FLOAT4
		{
			public float value0;
			public float value1;
			public float value2;
			public float value3;
			public float this[int index]
			{
				get => index switch
				{
					0 => value0,
					1 => value1,
					2 => value2,
					3 => value3,
					_ => throw new IndexOutOfRangeException()
				};
				set
				{
					switch (index)
					{
						case 0: value0 = value; break;
						case 1: value1 = value; break;
						case 2: value2 = value; break;
						case 3: value3 = value; break;
						default: throw new IndexOutOfRangeException();
					}
				}
			}
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct D3DX11_PASS_DESC
		{
			public IntPtr Name;                    // Name of this pass (nullptr if not anonymous)    
			public uint Annotations;           // Number of annotations on this pass

			public IntPtr pIAInputSignature;     // Signature from VS or GS (if there is no VS)
												   // or nullptr if neither exists
			public ulong IAInputSignatureSize;    // Singature size in bytes                                

			public uint StencilRef;            // Specified in SetDepthStencilState()
			public uint SampleMask;            // Specified in SetBlendState()
			public FLOAT4 BlendFactor;           // Specified in SetBlendState()
		};
		[StructLayout(LayoutKind.Sequential)]
		public struct D3DX11_EFFECT_DESC
		{
			public uint ConstantBuffers;        // Number of constant buffers in this effect
			public uint GlobalVariables;        // Number of global variables in this effect
			public uint InterfaceVariables;     // Number of global interfaces in this effect
			public uint Techniques;             // Number of techniques in this effect
			public uint Groups;                 // Number of groups in this effect
		};
		[StructLayout(LayoutKind.Sequential)]
		public struct D3DX11_EFFECT_VARIABLE_DESC
		{
			public IntPtr Name;               // Name of this variable, annotation, 
											  // or structure member
			public IntPtr Semantic;           // Semantic string of this variable
											  // or structure member (nullptr for 
											  // annotations or if not present)

			public uint Flags;              // D3DX11_EFFECT_VARIABLE_* flags
			public uint Annotations;        // Number of annotations on this variable
											// (always 0 for annotations)

			public uint BufferOffset;       // Offset into containing cbuffer or tbuffer
											// (always 0 for annotations or variables
											// not in constant buffers)

			public uint ExplicitBindPoint;  // Used if the variable has been explicitly bound
										 // using the register keyword. Check Flags for
										 // D3DX11_EFFECT_VARIABLE_EXPLICIT_BIND_POINT;
		};
		[StructLayout(LayoutKind.Sequential)]
		public struct D3DX11_EFFECT_TYPE_DESC
		{
			public IntPtr TypeName;               // Name of the type 
												  // (e.g. "float4" or "MyStruct")

			public uint Class;  // (e.g. scalar, vector, object, etc.)
			public uint Type;   // (e.g. float, texture, vertexshader, etc.)

			public uint Elements;           // Number of elements in this type
											// (0 if not an array) 
			public uint Members;            // Number of members
											// (0 if not a structure)
			public uint Rows;               // Number of rows in this type
											// (0 if not a numeric primitive)
			public uint Columns;            // Number of columns in this type
											// (0 if not a numeric primitive)

			public uint PackedSize;         // Number of bytes required to represent
											// this data type, when tightly packed
			public uint UnpackedSize;       // Number of bytes occupied by this data
											// type, when laid out in a constant buffer
			public uint Stride;             // Number of bytes to seek between elements,
										 // when laid out in a constant buffer
		};
		[StructLayout(LayoutKind.Sequential)]
		public struct D3DX11_GROUP_DESC
		{
			public IntPtr Name;                   // Name of this group (only nullptr if global)
			public uint Techniques;         // Number of techniques contained within
			public uint Annotations;        // Number of annotations on this group
		};
		[StructLayout(LayoutKind.Sequential)]
		public struct D3DX11_TECHNIQUE_DESC
		{
			public IntPtr Name;                   // Name of this technique (nullptr if not anonymous)
			public uint Passes;             // Number of passes contained within
			public uint Annotations;        // Number of annotations on this technique
		};
		[StructLayout(LayoutKind.Sequential)]
		public struct EffectDesc
		{
			public D3DX11_EFFECT_DESC ShaderDesc;
			public uint ConstantBufferCount;
			public IntPtr ConstantBufferDescs;
			public IntPtr Variables;
			public IntPtr VariableTypes;
			public IntPtr PVariables;
			public IntPtr Groups;
			public IntPtr Techniques;
			public IntPtr PTechniques;
			public IntPtr Passes;
			public IntPtr PPasses;
		};
	}
}
