#include "ossia_utils.hpp"
#include <iostream>
extern "C"
{
static ossia_log_fun_t log_fun;
void ossia_set_debug_logger(ossia_log_fun_t fp)
{
    log_fun = fp;
}

void ossia_log_error(const char* error)
{
    log_fun(error);
}

ossia_protocol_t ossia_protocol_local_create()
try
{
    return new ossia_protocol{OSSIA::Local::create()};
}
catch(...)
{
    DEBUG_LOG_FMT("Cannot create Local protocol");
    return nullptr;
}

ossia_protocol_t ossia_protocol_osc_create(
        const char* ip,
        int in_port,
        int out_port)
try
{
    return new ossia_protocol{OSSIA::OSC::create(ip, in_port, out_port)};
}
catch(...)
{
    DEBUG_LOG_FMT("Cannot create OSC protocol");
    return nullptr;
}

ossia_protocol_t ossia_protocol_minuit_create(
        const char* ip,
        int in_port,
        int out_port)
try
{
    return new ossia_protocol{OSSIA::Minuit::create(ip, in_port, out_port)};
}
catch(...)
{
    DEBUG_LOG_FMT("Cannot create Minuit protocol");
    return nullptr;
}
}
