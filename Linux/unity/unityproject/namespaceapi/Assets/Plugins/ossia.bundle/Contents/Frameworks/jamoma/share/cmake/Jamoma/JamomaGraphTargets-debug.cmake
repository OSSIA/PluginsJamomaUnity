#----------------------------------------------------------------
# Generated CMake target import file for configuration "Debug".
#----------------------------------------------------------------

# Commands may need to know the format version.
set(CMAKE_IMPORT_FILE_VERSION 1)

# Import target "Jamoma::Graph" for configuration "Debug"
set_property(TARGET Jamoma::Graph APPEND PROPERTY IMPORTED_CONFIGURATIONS DEBUG)
set_target_properties(Jamoma::Graph PROPERTIES
  IMPORTED_LOCATION_DEBUG "${_IMPORT_PREFIX}/lib/libJamomaGraph.6.dylib"
  IMPORTED_SONAME_DEBUG "@rpath/libJamomaGraph.6.dylib"
  )

list(APPEND _IMPORT_CHECK_TARGETS Jamoma::Graph )
list(APPEND _IMPORT_CHECK_FILES_FOR_Jamoma::Graph "${_IMPORT_PREFIX}/lib/libJamomaGraph.6.dylib" )

# Import target "Jamoma::DictionaryLib" for configuration "Debug"
set_property(TARGET Jamoma::DictionaryLib APPEND PROPERTY IMPORTED_CONFIGURATIONS DEBUG)
set_target_properties(Jamoma::DictionaryLib PROPERTIES
  IMPORTED_LOCATION_DEBUG "${_IMPORT_PREFIX}/extensions/DictionaryLib.ttdylib"
  IMPORTED_SONAME_DEBUG "@rpath/DictionaryLib.ttdylib"
  )

list(APPEND _IMPORT_CHECK_TARGETS Jamoma::DictionaryLib )
list(APPEND _IMPORT_CHECK_FILES_FOR_Jamoma::DictionaryLib "${_IMPORT_PREFIX}/extensions/DictionaryLib.ttdylib" )

# Import target "Jamoma::MidiLib" for configuration "Debug"
set_property(TARGET Jamoma::MidiLib APPEND PROPERTY IMPORTED_CONFIGURATIONS DEBUG)
set_target_properties(Jamoma::MidiLib PROPERTIES
  IMPORTED_LOCATION_DEBUG "${_IMPORT_PREFIX}/extensions/MidiLib.ttdylib"
  IMPORTED_SONAME_DEBUG "@rpath/MidiLib.ttdylib"
  )

list(APPEND _IMPORT_CHECK_TARGETS Jamoma::MidiLib )
list(APPEND _IMPORT_CHECK_FILES_FOR_Jamoma::MidiLib "${_IMPORT_PREFIX}/extensions/MidiLib.ttdylib" )

# Commands beyond this point should not need to know the version.
set(CMAKE_IMPORT_FILE_VERSION)
