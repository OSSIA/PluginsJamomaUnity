cmake_minimum_required(VERSION 3.4)

project(PluginJamomaUnity CXX)
find_package(Jamoma COMPONENTS Foundation Modular Score)

add_subdirectory(API)


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
