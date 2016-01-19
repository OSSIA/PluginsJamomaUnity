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
#include <cstdio>

#define DEBUG_LOG_FMT(fmt, ...)    \
    do { \
    static char buf[4096]; \
    sprintf(buf, fmt, ##__VA_ARGS__); \
    ossia_log_error(buf); \
    } while (0)

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

inline auto convert(ossia_type t)
{
    return static_cast<OSSIA::Value::Type>(t);
}

inline auto convert(ossia_access_mode t)
{
    return static_cast<OSSIA::Address::AccessMode>(t);
}

inline auto convert(ossia_bounding_mode t)
{
    return static_cast<OSSIA::Address::BoundingMode>(t);
}

inline auto convert(OSSIA::Value::Type t)
{
    return static_cast<ossia_type>(t);
}

inline auto convert(OSSIA::Address::AccessMode t)
{
    return static_cast<ossia_access_mode>(t);
}

inline auto convert(OSSIA::Address::BoundingMode t)
{
    return static_cast<ossia_bounding_mode>(t);
}

inline auto convert(const OSSIA::Value* v)
{
    return const_cast<ossia_value_t>(static_cast<const void*>(v));
}

inline auto convert(ossia_value_t v)
{
    return static_cast<OSSIA::Value*>(v);
}
