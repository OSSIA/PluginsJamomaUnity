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

#include <TTBase.h>

#include <algorithm>
#include <cstring>
#include <cstdio>
#include "tinyformat.h"

template<typename Str, typename... Args>
void DEBUG_LOG_FMT(Str fmt, Args... args)
{
    auto str = tfm::format(fmt, args...);
    ossia_log_error(str.c_str());
}

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


template<typename Fun>
auto safe_function(const char name[], Fun f)
try
{
    return f();
}
catch(TTException& e)
{
    DEBUG_LOG_FMT("%s: %s", name, e.getReason());
    return decltype(f())();
}
catch(const std::exception& e)
{
    DEBUG_LOG_FMT("%s: %s", name, e.what());
    return decltype(f())();
}
catch(...)
{
    DEBUG_LOG_FMT("%s: Exception caught", name);
    return decltype(f())();
}

