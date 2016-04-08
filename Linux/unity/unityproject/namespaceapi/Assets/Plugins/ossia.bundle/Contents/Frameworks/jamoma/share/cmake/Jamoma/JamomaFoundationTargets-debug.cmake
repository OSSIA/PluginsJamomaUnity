#----------------------------------------------------------------
# Generated CMake target import file for configuration "Debug".
#----------------------------------------------------------------

# Commands may need to know the format version.
set(CMAKE_IMPORT_FILE_VERSION 1)

# Import target "Jamoma::Foundation" for configuration "Debug"
set_property(TARGET Jamoma::Foundation APPEND PROPERTY IMPORTED_CONFIGURATIONS DEBUG)
set_target_properties(Jamoma::Foundation PROPERTIES
  IMPORTED_LOCATION_DEBUG "${_IMPORT_PREFIX}/lib/libJamomaFoundation.6.dylib"
  IMPORTED_SONAME_DEBUG "@rpath/libJamomaFoundation.6.dylib"
  )

list(APPEND _IMPORT_CHECK_TARGETS Jamoma::Foundation )
list(APPEND _IMPORT_CHECK_FILES_FOR_Jamoma::Foundation "${_IMPORT_PREFIX}/lib/libJamomaFoundation.6.dylib" )

# Import target "Jamoma::DataspaceLib" for configuration "Debug"
set_property(TARGET Jamoma::DataspaceLib APPEND PROPERTY IMPORTED_CONFIGURATIONS DEBUG)
set_target_properties(Jamoma::DataspaceLib PROPERTIES
  IMPORTED_LOCATION_DEBUG "${_IMPORT_PREFIX}/extensions/DataspaceLib.ttdylib"
  IMPORTED_SONAME_DEBUG "@rpath/DataspaceLib.ttdylib"
  )

list(APPEND _IMPORT_CHECK_TARGETS Jamoma::DataspaceLib )
list(APPEND _IMPORT_CHECK_FILES_FOR_Jamoma::DataspaceLib "${_IMPORT_PREFIX}/extensions/DataspaceLib.ttdylib" )

# Import target "Jamoma::MatrixProcessingLib" for configuration "Debug"
set_property(TARGET Jamoma::MatrixProcessingLib APPEND PROPERTY IMPORTED_CONFIGURATIONS DEBUG)
set_target_properties(Jamoma::MatrixProcessingLib PROPERTIES
  IMPORTED_LOCATION_DEBUG "${_IMPORT_PREFIX}/extensions/MatrixProcessingLib.ttdylib"
  IMPORTED_SONAME_DEBUG "@rpath/MatrixProcessingLib.ttdylib"
  )

list(APPEND _IMPORT_CHECK_TARGETS Jamoma::MatrixProcessingLib )
list(APPEND _IMPORT_CHECK_FILES_FOR_Jamoma::MatrixProcessingLib "${_IMPORT_PREFIX}/extensions/MatrixProcessingLib.ttdylib" )

# Import target "Jamoma::NetworkLib" for configuration "Debug"
set_property(TARGET Jamoma::NetworkLib APPEND PROPERTY IMPORTED_CONFIGURATIONS DEBUG)
set_target_properties(Jamoma::NetworkLib PROPERTIES
  IMPORTED_LOCATION_DEBUG "${_IMPORT_PREFIX}/extensions/NetworkLib.ttdylib"
  IMPORTED_SONAME_DEBUG "@rpath/NetworkLib.ttdylib"
  )

list(APPEND _IMPORT_CHECK_TARGETS Jamoma::NetworkLib )
list(APPEND _IMPORT_CHECK_FILES_FOR_Jamoma::NetworkLib "${_IMPORT_PREFIX}/extensions/NetworkLib.ttdylib" )

# Commands beyond this point should not need to know the version.
set(CMAKE_IMPORT_FILE_VERSION)
