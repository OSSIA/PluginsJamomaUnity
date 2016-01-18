#include "ossia.h"

#include <Network/Address.h>
#include <Network/Device.h>
#include <Network/Node.h>
#include <Network/Protocol.h>
#include <Network/Protocol/Local.h>
#include <Network/Protocol/OSC.h>
#include <Network/Protocol/Minuit.h>
#include <Editor/Value.h>

#include <algorithm>

struct ossia_protocol
{
        std::shared_ptr<OSSIA::Protocol> protocol;
};

struct ossia_device
{
        std::shared_ptr<OSSIA::Device> device;
};

struct ossia_node
{
        std::shared_ptr<OSSIA::Node> node;
};

struct ossia_address
{
        std::shared_ptr<OSSIA::Address> address;
};

struct ossia_value
{
        OSSIA::Value* value{};
};

struct ossia_domain
{
        std::shared_ptr<OSSIA::Address> domain;
};

auto convert(ossia_type t)
{
    return static_cast<OSSIA::Value::Type>(t);
}
auto convert(ossia_access_mode t)
{
    return static_cast<OSSIA::Address::AccessMode>(t);
}
auto convert(ossia_bounding_mode t)
{
    return static_cast<OSSIA::Address::BoundingMode>(t);
}
auto convert(OSSIA::Value::Type t)
{
    return static_cast<ossia_type>(t);
}
auto convert(OSSIA::Address::AccessMode t)
{
    return static_cast<ossia_access_mode>(t);
}
auto convert(OSSIA::Address::BoundingMode t)
{
    return static_cast<ossia_bounding_mode>(t);
}

ossia_protocol_t ossia_protocol_local_create()
{
    return new ossia_protocol{OSSIA::Local::create()};
}

ossia_protocol_t ossia_protocol_osc_create(
        const char* ip,
        int in_port,
        int out_port)
{
    return new ossia_protocol{OSSIA::OSC::create(ip, in_port, out_port)};
}

ossia_protocol_t ossia_protocol_minuit_create(
        const char* ip,
        int in_port,
        int out_port)
{
    return new ossia_protocol{OSSIA::Minuit::create(ip, in_port, out_port)};
}

ossia_device_t ossia_device_create(
        ossia_protocol_t protocol,
        const char* name)
{
    auto dev = new ossia_device{OSSIA::Device::create(protocol->protocol, name)};

    delete protocol;
    return dev;
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
        const char* name)
{
    if(!device)
        return;

    auto& cld = device->device->children();
    std::string node_name = name;
    auto it = std::find_if(cld.begin(), cld.end(),
                           [&] (const auto& node) {
        return node->getName() == node_name;
    });

    if(it != cld.end())
    {
        device->device->children().erase(it);
    }
}

void ossia_node_remove_child(
        ossia_node_t node,
        const char* name)
{
    if(!node)
        return;

    auto& cld = node->node->children();
    std::string node_name = name;
    auto it = std::find_if(cld.begin(), cld.end(),
                           [&] (const auto& node) {
        return node->getName() == node_name;
    });

    if(it != cld.end())
    {
        node->node->children().erase(it);
    }
}


ossia_node_t ossia_node_add_child(
        ossia_node_t node,
        const char* name)
{
    if(!node)
        return nullptr;

    auto it = node->node->emplace(node->node->children().end(), name);
    auto child_node = *it;

    return new ossia_node{child_node};
}

ossia_address_t ossia_node_create_address(
        ossia_node_t node,
        ossia_type type)
{
    if(!node)
        return nullptr;

    return new ossia_address{node->node->createAddress(convert(type))};
}

void ossia_node_remove_address(
        ossia_node_t node)
{
    if(!node)
        return;

    node->node->removeAddress();
}

void ossia_address_set_access_mode(
        ossia_address_t address,
        ossia_access_mode am)
{
    if(!address)
        return;

    address->address->setAccessMode(convert(am));
}

ossia_access_mode ossia_address_get_access_mode(ossia_address_t address)
{
    if(!address)
        return ossia_access_mode{};

    return convert(address->address->getAccessMode());
}


void ossia_address_set_bounding_mode(
        ossia_address_t address,
        ossia_bounding_mode am)
{
    if(!address)
        return;

    address->address->setBoundingMode(convert(am));
}

ossia_bounding_mode ossia_address_get_bounding_mode(ossia_address_t address)
{
    if(!address)
        return ossia_bounding_mode{};

    return convert(address->address->getBoundingMode());
}

