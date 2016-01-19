#include "ossia_utils.hpp"
#include <iostream>
extern "C"
{
ossia_device_t ossia_device_create(
        ossia_protocol_t protocol,
        const char* name)
try
{
    auto dev = new ossia_device{OSSIA::Device::create(protocol->protocol, name)};

    delete protocol;
    return dev;
}
catch(...)
{

}

void ossia_device_free(ossia_device_t device)
{
    delete device;
}

bool ossia_device_update_namespace(ossia_device_t device)
{
    if(device)
    {
        return device->device->updateNamespace();
    }
    else
    {
        return false;
    }
}


ossia_node_t ossia_device_add_child(
        ossia_device_t device,
        const char* name)
{
    if(!device)
        return nullptr;

    auto it = device->device->emplace(device->device->children().end(), name);
    auto child_node = *it;

    return new ossia_node{child_node};
}

void ossia_device_remove_child(
        ossia_device_t device,
        ossia_node_t child)
{
    if(!device)
        return;
    if(!child)
        return;

    auto& cld = device->device->children();
    std::string node_name = child->node->getName();
    auto it = std::find_if(cld.begin(), cld.end(),
                           [&] (const auto& node) {
        return node->getName() == node_name;
    });

    if(it != cld.end())
    {
        device->device->children().erase(it);
    }
    delete child;
}

int ossia_device_child_size(
        ossia_device_t device)
{
    if(!device)
        return {};

    return device->device->children().size();
}

ossia_node_t ossia_device_get_child(
        ossia_device_t device,
        int child_n)
{
    if(!device)
        return {};

    if(device->device->children().size() < child_n)
        return nullptr;

    return new ossia_node{device->device->children()[child_n]};
}
}
