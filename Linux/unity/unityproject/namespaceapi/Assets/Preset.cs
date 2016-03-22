using UnityEngine;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.InteropServices;
using System;
using Ossia;

namespace Namespace {

	unsafe internal class BlueYetiAPI {

		/* Preset handling */

		[DllImport ("ossia")]
		public static extern ossia_preset_result_enum ossia_presets_read_json (string str, IntPtr * preset);

		[DllImport ("ossia")]
		public static extern ossia_preset_result_enum ossia_presets_read_xml (string str, IntPtr * preset);

		[DllImport ("ossia")]
		public static extern ossia_preset_result_enum ossia_presets_free (IntPtr preset);

		[DllImport ("ossia")]
		public static extern ossia_preset_result_enum ossia_presets_write_json (IntPtr preset, IntPtr* buffer);

		[DllImport ("ossia")]
		public static extern ossia_preset_result_enum ossia_presets_write_xml (IntPtr preset, IntPtr* buffer);

		[DllImport ("ossia")]
		public static extern ossia_preset_result_enum ossia_presets_size(IntPtr preset, int* sizebuffer);

		[DllImport ("ossia")]
		public static extern ossia_preset_result_enum ossia_presets_to_string (IntPtr preset, IntPtr* buffer);

		/* Devices handling */

		[DllImport ("ossia")]
		public static extern ossia_preset_result_enum ossia_devices_read_json (IntPtr * ossia_device, string str);

		[DllImport ("ossia")]
		public static extern ossia_preset_result_enum ossia_devices_read_xml (IntPtr * ossia_device, string str);

		[DllImport ("ossia")]
		public static extern ossia_preset_result_enum ossia_devices_write_json (IntPtr ossia_device, IntPtr* buffer);

		[DllImport ("ossia")]
		public static extern ossia_preset_result_enum ossia_devices_write_xml (IntPtr ossia_device, IntPtr* buffer);

		[DllImport ("ossia")]
		public static extern ossia_preset_result_enum ossia_devices_apply_preset (IntPtr ossia_device, IntPtr preset, bool keep_arch);

		[DllImport ("ossia")]
		public static extern ossia_preset_result_enum ossia_devices_make_preset (IntPtr ossia_device, IntPtr * preset);

		[DllImport ("ossia")]
		public static extern ossia_preset_result_enum ossia_devices_to_string (IntPtr ossia_device, IntPtr* buffer);

		[DllImport ("ossia")]
		public static extern void ossia_preset_set_debug_logger( IntPtr fp );

		/* Miscellaneous */

		[DllImport ("ossia")]
		public static extern ossia_preset_result_enum ossia_devices_get_node (IntPtr ossia_device, string nodekeys, IntPtr* nodebuffer);

		[DllImport ("ossia")]
		public static extern ossia_preset_result_enum ossia_devices_get_child (IntPtr rootnode, string childname, IntPtr* nodebuffer);

		[DllImport ("ossia")]
		public static extern ossia_preset_result_enum ossia_preset_free_string (IntPtr str);
	}

	unsafe public class Preset {

		internal IntPtr preset;

		public Preset() {}

		public Preset(string json) {
			fixed (IntPtr* presetptr = &preset) {
				ossia_preset_result_enum code = BlueYetiAPI.ossia_presets_read_json (json, presetptr);
				if (code != ossia_preset_result_enum.OSSIA_PRESETS_OK) {
					throw new Exception ("Error code " + code);
				}
			}
		}
		public string WriteJson() {
			IntPtr ptr;
			ossia_preset_result_enum code = BlueYetiAPI.ossia_presets_write_json (preset, &ptr);
			if (code == ossia_preset_result_enum.OSSIA_PRESETS_OK) {
				string str = Marshal.PtrToStringAuto (ptr);
				Debug.Log ("Wrote json \"" + str + "\"");
				BlueYetiAPI.ossia_preset_free_string (ptr);
				return str;
			} else {
				throw new Exception ("Error code " + code);
			}
		}

		public override System.String ToString() {
			IntPtr strptr;
			ossia_preset_result_enum code = BlueYetiAPI.ossia_presets_to_string (preset, &strptr);
			if (code == ossia_preset_result_enum.OSSIA_PRESETS_OK) {
				System.String str = Marshal.PtrToStringAuto (strptr);
				BlueYetiAPI.ossia_preset_free_string (strptr);
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
				ossia_preset_result_enum code = BlueYetiAPI.ossia_presets_size (preset, &s);
				if (code == ossia_preset_result_enum.OSSIA_PRESETS_OK) {
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
				//Debug.Log (dev.GetDevice ());
				ossia_preset_result_enum code = ossia_preset_result_enum.OSSIA_PRESETS_OK;
				code = BlueYetiAPI.ossia_devices_apply_preset (dev.GetDevice(), preset, KeepArch);
				if (code != ossia_preset_result_enum.OSSIA_PRESETS_OK) {
					throw new Exception ("Error code " + code);
				}
			} else {
				throw new Exception ("Can't apply preset to null device");
			}
		}

		public void Free () {
			if (!IsNull ()) {
				ossia_preset_result_enum code = BlueYetiAPI.ossia_presets_free (preset);
				if (code != ossia_preset_result_enum.OSSIA_PRESETS_OK) {
					throw new Exception ("Error code " + code);
				} else {
					Debug.Log ("Freed preset");
				}
			}
		}
	}
}