#include "ossia_utils.hpp"

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
}
