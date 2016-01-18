cmake_minimum_required(VERSION 3.4)

project(PluginJamomaUnity CXX)
find_package(Jamoma COMPONENTS Foundation Modular Score)

add_subdirectory(API)

add_library(ossia
  src/ossia.h
  src/ossia.cpp
)
set_property(TARGET ossia PROPERTY CXX_STANDARD 14)
target_link_libraries(ossia APIJamoma)


add_library(PluginOssiaUnity SHARED
  src/PluginJamomaUnity.cpp
  src/PluginJamomaUnity.h
)

set_property(TARGET PluginOssiaUnity PROPERTY CXX_STANDARD 14)

target_link_libraries(PluginOssiaUnity Jamoma::Foundation Jamoma::Modular Jamoma::Score)
