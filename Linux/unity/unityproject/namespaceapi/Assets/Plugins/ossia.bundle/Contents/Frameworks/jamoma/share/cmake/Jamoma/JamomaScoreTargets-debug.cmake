#----------------------------------------------------------------
# Generated CMake target import file for configuration "Debug".
#----------------------------------------------------------------

# Commands may need to know the format version.
set(CMAKE_IMPORT_FILE_VERSION 1)

# Import target "Jamoma::Score" for configuration "Debug"
set_property(TARGET Jamoma::Score APPEND PROPERTY IMPORTED_CONFIGURATIONS DEBUG)
set_target_properties(Jamoma::Score PROPERTIES
  IMPORTED_LOCATION_DEBUG "${_IMPORT_PREFIX}/lib/libJamomaScore.6.dylib"
  IMPORTED_SONAME_DEBUG "@rpath/libJamomaScore.6.dylib"
  )

list(APPEND _IMPORT_CHECK_TARGETS Jamoma::Score )
list(APPEND _IMPORT_CHECK_FILES_FOR_Jamoma::Score "${_IMPORT_PREFIX}/lib/libJamomaScore.6.dylib" )

# Import target "Jamoma::Automation" for configuration "Debug"
set_property(TARGET Jamoma::Automation APPEND PROPERTY IMPORTED_CONFIGURATIONS DEBUG)
set_target_properties(Jamoma::Automation PROPERTIES
  IMPORTED_LOCATION_DEBUG "${_IMPORT_PREFIX}/extensions/Automation.ttdylib"
  IMPORTED_SONAME_DEBUG "@rpath/Automation.ttdylib"
  )

list(APPEND _IMPORT_CHECK_TARGETS Jamoma::Automation )
list(APPEND _IMPORT_CHECK_FILES_FOR_Jamoma::Automation "${_IMPORT_PREFIX}/extensions/Automation.ttdylib" )

# Import target "Jamoma::Interval" for configuration "Debug"
set_property(TARGET Jamoma::Interval APPEND PROPERTY IMPORTED_CONFIGURATIONS DEBUG)
set_target_properties(Jamoma::Interval PROPERTIES
  IMPORTED_LOCATION_DEBUG "${_IMPORT_PREFIX}/extensions/Interval.ttdylib"
  IMPORTED_SONAME_DEBUG "@rpath/Interval.ttdylib"
  )

list(APPEND _IMPORT_CHECK_TARGETS Jamoma::Interval )
list(APPEND _IMPORT_CHECK_FILES_FOR_Jamoma::Interval "${_IMPORT_PREFIX}/extensions/Interval.ttdylib" )

# Import target "Jamoma::Loop" for configuration "Debug"
set_property(TARGET Jamoma::Loop APPEND PROPERTY IMPORTED_CONFIGURATIONS DEBUG)
set_target_properties(Jamoma::Loop PROPERTIES
  IMPORTED_LOCATION_DEBUG "${_IMPORT_PREFIX}/extensions/Loop.ttdylib"
  IMPORTED_SONAME_DEBUG "@rpath/Loop.ttdylib"
  )

list(APPEND _IMPORT_CHECK_TARGETS Jamoma::Loop )
list(APPEND _IMPORT_CHECK_FILES_FOR_Jamoma::Loop "${_IMPORT_PREFIX}/extensions/Loop.ttdylib" )

# Import target "Jamoma::Scenario" for configuration "Debug"
set_property(TARGET Jamoma::Scenario APPEND PROPERTY IMPORTED_CONFIGURATIONS DEBUG)
set_target_properties(Jamoma::Scenario PROPERTIES
  IMPORTED_LOCATION_DEBUG "${_IMPORT_PREFIX}/extensions/Scenario.ttdylib"
  IMPORTED_SONAME_DEBUG "@rpath/Scenario.ttdylib"
  )

list(APPEND _IMPORT_CHECK_TARGETS Jamoma::Scenario )
list(APPEND _IMPORT_CHECK_FILES_FOR_Jamoma::Scenario "${_IMPORT_PREFIX}/extensions/Scenario.ttdylib" )

# Commands beyond this point should not need to know the version.
set(CMAKE_IMPORT_FILE_VERSION)
