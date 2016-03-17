using UnityEngine;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.InteropServices;
using System;
using Ossia;

namespace Namespace {

	unsafe internal class BlueYetiAPI {

		/* Preset handling */

		[DllImport ("libnamespaceapi")]
		public static extern blueyeti_result_enum blueyeti_presets_read_json (string str, IntPtr * preset);

		[DllImport ("libnamespaceapi")]
		public static extern blueyeti_result_enum blueyeti_presets_read_xml (string str, IntPtr * preset);

		[DllImport ("libnamespaceapi")]
		public static extern blueyeti_result_enum blueyeti_presets_free (IntPtr preset);

		[DllImport ("libnamespaceapi")]
		public static extern blueyeti_result_enum blueyeti_presets_write_json (IntPtr preset, IntPtr* buffer);

		[DllImport ("libnamespaceapi")]
		public static extern blueyeti_result_enum blueyeti_presets_write_xml (IntPtr preset, IntPtr* buffer);

		[DllImport ("libnamespaceapi")]
		public static extern blueyeti_result_enum blueyeti_presets_size(IntPtr preset, int* sizebuffer);

		[DllImport ("libnamespaceapi")]
		public static extern blueyeti_result_enum blueyeti_presets_to_string (IntPtr preset, IntPtr* buffer);

		/* Devices handling */

		[DllImport ("libnamespaceapi")]
		public static extern blueyeti_result_enum blueyeti_devices_read_json (IntPtr * ossia_device, string str);

		[DllImport ("libnamespaceapi")]
		public static extern blueyeti_result_enum blueyeti_devices_read_xml (IntPtr * ossia_device, string str);

		[DllImport ("libnamespaceapi")]
		public static extern blueyeti_result_enum blueyeti_devices_write_json (IntPtr ossia_device, IntPtr* buffer);

		[DllImport ("libnamespaceapi")]
		public static extern blueyeti_result_enum blueyeti_devices_write_xml (IntPtr ossia_device, IntPtr* buffer);

		[DllImport ("libnamespaceapi")]
		public static extern blueyeti_result_enum blueyeti_devices_apply_preset (IntPtr ossia_device, IntPtr preset, bool keep_arch);

		[DllImport ("libnamespaceapi")]
		public static extern blueyeti_result_enum blueyeti_devices_make_preset (IntPtr ossia_device, IntPtr * preset);

		[DllImport ("libnamespaceapi")]
		public static extern blueyeti_result_enum blueyeti_devices_to_string (IntPtr ossia_device, IntPtr* buffer);

		[DllImport ("libnamespaceapi")]
		public static extern void blueyeti_set_debug_logger( IntPtr fp );
		/* Miscellaneous */

		[DllImport ("libnamespaceapi")]
		public static extern blueyeti_result_enum blueyeti_free_string (IntPtr str);
	}

	unsafe public class Preset {

		internal IntPtr preset;

		public Preset() {}

		public Preset(string json) {
			fixed (IntPtr* presetptr = &preset) {
				blueyeti_result_enum code = BlueYetiAPI.blueyeti_presets_read_json (json, presetptr);
				if (code != blueyeti_result_enum.BLUEYETI_OK) {
					throw new Exception ("Error code " + code);
				}
				Debug.Log (preset);
			}
		}
		public string WriteJson() {
			IntPtr ptr;
			blueyeti_result_enum code = BlueYetiAPI.blueyeti_presets_write_json (preset, &ptr);
			if (code == blueyeti_result_enum.BLUEYETI_OK) {
				string str = Marshal.PtrToStringAuto (ptr);
				Debug.Log ("Wrote json \"" + str + "\"");
				BlueYetiAPI.blueyeti_free_string (ptr);
				return str;
			} else {
				throw new Exception ("Error code " + code);
			}
		}

		public override System.String ToString() {
			IntPtr strptr;
			blueyeti_result_enum code = BlueYetiAPI.blueyeti_presets_to_string (preset, &strptr);
			if (code == blueyeti_result_enum.BLUEYETI_OK) {
				System.String str = System.String.Copy (Marshal.PtrToStringAuto (strptr));
				BlueYetiAPI.blueyeti_free_string (strptr);
				return str;
			} else {
				throw new Exception ("Error code " + code);
			}
		}

		public int Size() {
			if (IsNull()) {
				return -1;
			}
			else {
				int s;
				blueyeti_result_enum code = BlueYetiAPI.blueyeti_presets_size (preset, &s);
				if (code == blueyeti_result_enum.BLUEYETI_OK) {
					return s;
				} else {
					throw new Exception ("Error code " + code);
				}
			}
		}

		public bool IsNull() {
			return preset == IntPtr.Zero;
		}

		public void ApplyToDevice(Ossia.Device dev, bool KeepArch) {
			if (dev.GetDevice() != IntPtr.Zero) {
				Debug.Log (dev.GetDevice ());
				blueyeti_result_enum code = blueyeti_result_enum.BLUEYETI_OK;
				code = BlueYetiAPI.blueyeti_devices_apply_preset (dev.GetDevice(), preset, KeepArch);
				if (code != blueyeti_result_enum.BLUEYETI_OK) {
					throw new Exception ("Error code " + code);
				}
			} else {
				throw new Exception ("Can't apply preset to null device");
			}
		}
	}
}