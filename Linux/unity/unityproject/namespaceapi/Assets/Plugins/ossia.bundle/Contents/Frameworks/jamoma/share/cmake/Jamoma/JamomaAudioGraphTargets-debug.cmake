#----------------------------------------------------------------
# Generated CMake target import file for configuration "Debug".
#----------------------------------------------------------------

# Commands may need to know the format version.
set(CMAKE_IMPORT_FILE_VERSION 1)

# Import target "Jamoma::AudioGraph" for configuration "Debug"
set_property(TARGET Jamoma::AudioGraph APPEND PROPERTY IMPORTED_CONFIGURATIONS DEBUG)
set_target_properties(Jamoma::AudioGraph PROPERTIES
  IMPORTED_LOCATION_DEBUG "${_IMPORT_PREFIX}/lib/libJamomaAudioGraph.6.dylib"
  IMPORTED_SONAME_DEBUG "@rpath/libJamomaAudioGraph.6.dylib"
  )

list(APPEND _IMPORT_CHECK_TARGETS Jamoma::AudioGraph )
list(APPEND _IMPORT_CHECK_FILES_FOR_Jamoma::AudioGraph "${_IMPORT_PREFIX}/lib/libJamomaAudioGraph.6.dylib" )

# Import target "Jamoma::AudioGraphUtilityLib" for configuration "Debug"
set_property(TARGET Jamoma::AudioGraphUtilityLib APPEND PROPERTY IMPORTED_CONFIGURATIONS DEBUG)
set_target_properties(Jamoma::AudioGraphUtilityLib PROPERTIES
  IMPORTED_LOCATION_DEBUG "${_IMPORT_PREFIX}/extensions/AudioGraphUtilityLib.ttdylib"
  IMPORTED_SONAME_DEBUG "@rpath/AudioGraphUtilityLib.ttdylib"
  )

list(APPEND _IMPORT_CHECK_TARGETS Jamoma::AudioGraphUtilityLib )
list(APPEND _IMPORT_CHECK_FILES_FOR_Jamoma::AudioGraphUtilityLib "${_IMPORT_PREFIX}/extensions/AudioGraphUtilityLib.ttdylib" )

# Import target "Jamoma::Plugtastic" for configuration "Debug"
set_property(TARGET Jamoma::Plugtastic APPEND PROPERTY IMPORTED_CONFIGURATIONS DEBUG)
set_target_properties(Jamoma::Plugtastic PROPERTIES
  IMPORTED_LOCATION_DEBUG "${_IMPORT_PREFIX}/extensions/Plugtastic.ttdylib"
  IMPORTED_SONAME_DEBUG "@rpath/Plugtastic.ttdylib"
  )

list(APPEND _IMPORT_CHECK_TARGETS Jamoma::Plugtastic )
list(APPEND _IMPORT_CHECK_FILES_FOR_Jamoma::Plugtastic "${_IMPORT_PREFIX}/extensions/Plugtastic.ttdylib" )

# Commands beyond this point should not need to know the version.
set(CMAKE_IMPORT_FILE_VERSION)
