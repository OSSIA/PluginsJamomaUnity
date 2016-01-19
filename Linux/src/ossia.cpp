#include "ossia.h"

#include <Network/Address.h>
#include <Network/Device.h>
#include <Network/Node.h>
#include <Network/Protocol.h>
#include <Network/Protocol/Local.h>
#include <Network/Protocol/OSC.h>
#include <Network/Protocol/Minuit.h>
#include <Editor/Value.h>
#include <Editor/Domain.h>

#include <algorithm>
#include <cstring>

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

struct ossia_domain
{
        std::shared_ptr<OSSIA::Domain> domain;
};

struct ossia_value_callback_index
{
        OSSIA::Address::iterator it;
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
auto convert(const OSSIA::Value* v)
{
    return const_cast<ossia_value_t>(static_cast<const void*>(v));
}
auto convert(ossia_value_t v)
{
    return static_cast<OSSIA::Value*>(v);
}


extern "C"
{
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

void ossia_node_remove_child(
        ossia_node_t node,
        ossia_node_t child)
{
    if(!node)
        return;
    if(!child)
        return;

    auto& cld = node->node->children();
    std::string node_name = child->node->getName();
    auto it = std::find_if(cld.begin(), cld.end(),
                           [&] (const auto& node) {
        return node->getName() == node_name;
    });

    if(it != cld.end())
    {
        node->node->children().erase(it);
    }
    delete child;
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

ossia_bounding_mode ossia_address_get_bounding_mode(
        ossia_address_t address)
{
    if(!address)
        return ossia_bounding_mode{};

    return convert(address->address->getBoundingMode());
}


void ossia_address_set_domain(
        ossia_address_t address,
        ossia_domain_t domain)
{
    if(!address)
        return;
    if(!domain)
        return;

    address->address->setDomain(domain->domain);
}

ossia_domain_t ossia_address_get_domain(
        ossia_address_t address)
{
    if(!address)
        return nullptr;

    return new ossia_domain{address->address->getDomain()};
}

void ossia_address_set_value(
        ossia_address_t address,
        ossia_value_t value)
{
    if(!address)
        return;
    if(!value)
        return;

    address->address->setValue(convert(value));
}

ossia_value_t ossia_address_get_value(
        ossia_address_t address)
{
    if(!address)
        return nullptr;

    return convert(address->address->getValue());
}

void ossia_address_push_value(
        ossia_address_t address,
        ossia_value_t value)
{
    if(!address)
        return;
    if(!value)
        return;

    address->address->pushValue(convert(value));
}

ossia_value_t ossia_address_pull_value(
        ossia_address_t address)
{
    if(!address)
        return nullptr;

    return convert(address->address->pullValue());
}

ossia_value_callback_index_t ossia_address_add_callback(
        ossia_address_t address,
        ossia_value_callback_t callback)
{
    if(!address)
        return nullptr;
    if(!callback)
        return nullptr;

    return new ossia_value_callback_index{
        address->address->addCallback([=] (const OSSIA::Value* val) {
            callback(convert(val));
        })
    };
}

void ossia_address_remove_callback(
        ossia_address_t address,
        ossia_value_callback_index_t index)
{
    if(!address)
        return;
    if(!index)
        return;

    address->address->removeCallback(index->it);
    delete index;
}

ossia_value_t ossia_domain_get_min(
        ossia_domain_t domain)
{
    if(!domain)
        return nullptr;

    return convert(domain->domain->getMin());
}

void ossia_domain_set_min(
        ossia_domain_t domain,
        ossia_value_t value)
{
    if(!domain)
        return;
    if(!value)
        return;

    domain->domain->setMin(convert(value));
}

ossia_value_t ossia_domain_get_max(
        ossia_domain_t domain)
{
    if(!domain)
        return nullptr;

    return convert(domain->domain->getMax());
}

void ossia_domain_set_max(
        ossia_domain_t domain,
        ossia_value_t value)
{
    if(!domain)
        return;
    if(!value)
        return;

    domain->domain->setMax(convert(value));
}

void ossia_domain_free(ossia_domain_t domain)
{
    delete domain;
}

ossia_value_t ossia_value_create_impulse()
{
    return convert(new OSSIA::Impulse);
}

ossia_value_t ossia_value_create_int(
        int value)
{
    return convert(new OSSIA::Int{value});
}

ossia_value_t ossia_value_create_float(
        float value)
{
    return convert(new OSSIA::Float{value});
}

ossia_value_t ossia_value_create_bool(
        bool value)
{
    return convert(new OSSIA::Bool{value});
}

ossia_value_t ossia_value_create_char(
        char value)
{
    return convert(new OSSIA::Char{value});
}

ossia_value_t ossia_value_create_string(
        const char* value)
{
    return convert(new OSSIA::String{value});
}

ossia_value_t ossia_value_create_tuple(
        ossia_value_t* values,
        int size)
{
    auto tuple = new OSSIA::Tuple;
    for(int i = 0; i < size; i++)
    {
        tuple->value.push_back(convert(values[i]));
    }
    return convert(tuple);
}

void ossia_value_free(ossia_value_t value)
{
    delete convert(value);
}

ossia_type ossia_value_get_type(
        ossia_value_t val)
{
    return convert(convert(val)->getType());
}

int ossia_value_to_int(
        ossia_value_t val)
{
    if(!val)
        return {};

    OSSIA::Value* ossia_val = convert(val);
    if(auto casted_val = dynamic_cast<OSSIA::Int*>(ossia_val))
    {
        return casted_val->value;
    }

    return {};
}

float ossia_value_to_float(
        ossia_value_t val)
{
    if(!val)
        return {};

    OSSIA::Value* ossia_val = convert(val);
    if(auto casted_val = dynamic_cast<OSSIA::Float*>(ossia_val))
    {
        return casted_val->value;
    }

    return {};
}

bool ossia_value_to_bool(
        ossia_value_t val)
{
    if(!val)
        return {};

    OSSIA::Value* ossia_val = convert(val);
    if(auto casted_val = dynamic_cast<OSSIA::Bool*>(ossia_val))
    {
        return casted_val->value;
    }

    return {};
}

char ossia_value_to_char(
        ossia_value_t val)
{
    if(!val)
        return {};

    OSSIA::Value* ossia_val = convert(val);
    if(auto casted_val = dynamic_cast<OSSIA::Char*>(ossia_val))
    {
        return casted_val->value;
    }

    return {};
}

const char* ossia_value_to_string(
        ossia_value_t val)
{
    if(!val)
        return nullptr;

    OSSIA::Value* ossia_val = convert(val);
    if(auto casted_val = dynamic_cast<OSSIA::String*>(ossia_val))
    {
        auto& s = casted_val->value;
        auto size = s.size();
        char *buf = new char[size + 1];
        std::strcpy(buf, s.c_str());
        return buf;
    }

    return nullptr;
}

void ossia_value_free_string(const char * str)
{
    delete[] str;
}

void ossia_value_to_tuple(
        ossia_value_t val_in,
        ossia_value_t* out,
        int* size)
{

}

}


