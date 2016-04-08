#----------------------------------------------------------------
# Generated CMake target import file for configuration "Debug".
#----------------------------------------------------------------

# Commands may need to know the format version.
set(CMAKE_IMPORT_FILE_VERSION 1)

# Import target "Jamoma::Modular" for configuration "Debug"
set_property(TARGET Jamoma::Modular APPEND PROPERTY IMPORTED_CONFIGURATIONS DEBUG)
set_target_properties(Jamoma::Modular PROPERTIES
  IMPORTED_LOCATION_DEBUG "${_IMPORT_PREFIX}/lib/libJamomaModular.6.dylib"
  IMPORTED_SONAME_DEBUG "@rpath/libJamomaModular.6.dylib"
  )

list(APPEND _IMPORT_CHECK_TARGETS Jamoma::Modular )
list(APPEND _IMPORT_CHECK_FILES_FOR_Jamoma::Modular "${_IMPORT_PREFIX}/lib/libJamomaModular.6.dylib" )

# Import target "Jamoma::MIDI" for configuration "Debug"
set_property(TARGET Jamoma::MIDI APPEND PROPERTY IMPORTED_CONFIGURATIONS DEBUG)
set_target_properties(Jamoma::MIDI PROPERTIES
  IMPORTED_LOCATION_DEBUG "${_IMPORT_PREFIX}/extensions/MIDI.ttdylib"
  IMPORTED_SONAME_DEBUG "@rpath/MIDI.ttdylib"
  )

list(APPEND _IMPORT_CHECK_TARGETS Jamoma::MIDI )
list(APPEND _IMPORT_CHECK_FILES_FOR_Jamoma::MIDI "${_IMPORT_PREFIX}/extensions/MIDI.ttdylib" )

# Import target "Jamoma::Minuit" for configuration "Debug"
set_property(TARGET Jamoma::Minuit APPEND PROPERTY IMPORTED_CONFIGURATIONS DEBUG)
set_target_properties(Jamoma::Minuit PROPERTIES
  IMPORTED_LOCATION_DEBUG "${_IMPORT_PREFIX}/extensions/Minuit.ttdylib"
  IMPORTED_SONAME_DEBUG "@rpath/Minuit.ttdylib"
  )

list(APPEND _IMPORT_CHECK_TARGETS Jamoma::Minuit )
list(APPEND _IMPORT_CHECK_FILES_FOR_Jamoma::Minuit "${_IMPORT_PREFIX}/extensions/Minuit.ttdylib" )

# Import target "Jamoma::OSC" for configuration "Debug"
set_property(TARGET Jamoma::OSC APPEND PROPERTY IMPORTED_CONFIGURATIONS DEBUG)
set_target_properties(Jamoma::OSC PROPERTIES
  IMPORTED_LOCATION_DEBUG "${_IMPORT_PREFIX}/extensions/OSC.ttdylib"
  IMPORTED_SONAME_DEBUG "@rpath/OSC.ttdylib"
  )

list(APPEND _IMPORT_CHECK_TARGETS Jamoma::OSC )
list(APPEND _IMPORT_CHECK_FILES_FOR_Jamoma::OSC "${_IMPORT_PREFIX}/extensions/OSC.ttdylib" )

# Import target "Jamoma::System" for configuration "Debug"
set_property(TARGET Jamoma::System APPEND PROPERTY IMPORTED_CONFIGURATIONS DEBUG)
set_target_properties(Jamoma::System PROPERTIES
  IMPORTED_LOCATION_DEBUG "${_IMPORT_PREFIX}/extensions/System.ttdylib"
  IMPORTED_SONAME_DEBUG "@rpath/System.ttdylib"
  )

list(APPEND _IMPORT_CHECK_TARGETS Jamoma::System )
list(APPEND _IMPORT_CHECK_FILES_FOR_Jamoma::System "${_IMPORT_PREFIX}/extensions/System.ttdylib" )

# Import target "Jamoma::WebSocket" for configuration "Debug"
set_property(TARGET Jamoma::WebSocket APPEND PROPERTY IMPORTED_CONFIGURATIONS DEBUG)
set_target_properties(Jamoma::WebSocket PROPERTIES
  IMPORTED_LOCATION_DEBUG "${_IMPORT_PREFIX}/extensions/WebSocket.ttdylib"
  IMPORTED_SONAME_DEBUG "@rpath/WebSocket.ttdylib"
  )

list(APPEND _IMPORT_CHECK_TARGETS Jamoma::WebSocket )
list(APPEND _IMPORT_CHECK_FILES_FOR_Jamoma::WebSocket "${_IMPORT_PREFIX}/extensions/WebSocket.ttdylib" )

# Commands beyond this point should not need to know the version.
set(CMAKE_IMPORT_FILE_VERSION)
