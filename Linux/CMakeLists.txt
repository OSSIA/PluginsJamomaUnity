cmake_minimum_required(VERSION 3.4)

project(PluginJamomaUnity CXX)
find_package(Jamoma COMPONENTS Foundation Modular Score)
set(BUILD_SHARED_LIBS On)

add_subdirectory(API)

add_library(ossia SHARED
  src/ossia.h
  src/ossia_utils.hpp
  src/ossia_protocol.cpp
  src/ossia_node.cpp
  src/ossia_device.cpp
  src/ossia_address.cpp
  src/ossia_value.cpp
  src/ossia_domain.cpp
)
set_property(TARGET ossia PROPERTY CXX_STANDARD 14)
target_link_libraries(ossia PRIVATE APIJamoma)


add_library(PluginOssiaUnity SHARED
  src/PluginJamomaUnity.cpp
  src/PluginJamomaUnity.h
)

set_target_properties(PluginOssiaUnity PROPERTIES
    CXX_STANDARD 14
    C_VISIBILITY_PRESET default
    CXX_VISIBILITY_PRESET default
)

target_link_libraries(PluginOssiaUnity Jamoma::Foundation Jamoma::Modular Jamoma::Score)


add_executable(test_reception
    test/test_reception.cpp)
set_property(TARGET test_reception PROPERTY CXX_STANDARD 14)
target_link_libraries(test_reception PRIVATE ossia)
