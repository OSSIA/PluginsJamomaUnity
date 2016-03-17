cmake_minimum_required(VERSION 3.4)

project(PluginJamomaUnity CXX)
enable_testing()
find_package(Jamoma COMPONENTS Foundation)
# Setup
set(BUILD_SHARED_LIBS On)
set(CMAKE_POSITION_INDEPENDENT_CODE True)

add_subdirectory(API)

add_library(ossia SHARED
  src/ossia/ossia.h
  src/ossia/ossia_utils.hpp
  src/ossia/ossia_protocol.cpp
  src/ossia/ossia_node.cpp
  src/ossia/ossia_device.cpp
  src/ossia/ossia_address.cpp
  src/ossia/ossia_value.cpp
  src/ossia/ossia_domain.cpp

  src/common/ossia_log.h
  src/common/ossia_log.cpp

  src/preset/preset.h
  src/preset/preset.hpp
  src/preset/preset.cpp
  src/preset/exception.hpp
  src/preset/exception.cpp
  src/preset/result.cs
)

# Main library
set_property(TARGET ossia PROPERTY CXX_STANDARD 14)
target_link_libraries(ossia PUBLIC APIJamoma Jamoma::Foundation)
target_compile_definitions(ossia PUBLIC RAPIDJSON_HAS_STDSTRING=1)
target_include_directories(ossia PUBLIC API/Headers)
target_include_directories(ossia PUBLIC src)

# Tests
function(add_ossia_lib_example TheTarget TheFile)
  add_executable(${TheTarget} ${TheFile})
  set_property(TARGET ${TheTarget} PROPERTY CXX_STANDARD 14)
  target_link_libraries(${TheTarget} PRIVATE ossia)
endfunction()
function(add_ossia_lib_test  TheTarget TheFile)
  add_ossia_lib_example(${TheTarget} ${TheFile})
  add_test(${TheTarget}_test ${TheTarget})
endfunction()


add_ossia_lib_example(ParsingExample examples/preset/ParsingExample.cpp)
add_ossia_lib_example(DeviceExample examples/preset/DeviceExample.cpp)

add_ossia_lib_test(test_reception test/ossia/test_reception.cpp)

add_ossia_lib_test(unit-tests-devices test/preset/unit-tests/unit-tests-devices.cpp)
add_ossia_lib_test(unit-tests-presets test/preset/unit-tests/unit-tests-presets.cpp)
add_ossia_lib_test(unit-tests-c-api test/preset/unit-tests/unit-tests-c-api.cpp)

include(CTest)
