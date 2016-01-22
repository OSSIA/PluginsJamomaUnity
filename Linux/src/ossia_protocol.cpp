#include "ossia_utils.hpp"
#include <iostream>
extern "C"
{
static ossia_log_fun_t log_fun = nullptr;
void ossia_set_debug_logger(ossia_log_fun_t fp)
{
    log_fun = fp;
}

void ossia_log_error(const char* error)
{
    if(log_fun)
        log_fun(error);
}

ossia_protocol_t ossia_protocol_local_create()
{
    return safe_function(__func__, [=] {
        return new ossia_protocol{OSSIA::Local::create()};
    });
}

ossia_protocol_t ossia_protocol_osc_create(
        const char* ip,
        int in_port,
        int out_port)
{
    return safe_function(__func__, [=] {
        return new ossia_protocol{OSSIA::OSC::create(ip, in_port, out_port)};
    });
}

ossia_protocol_t ossia_protocol_minuit_create(
        const char* ip,
        int in_port,
        int out_port)
{
    return safe_function(__func__, [=] {
        return new ossia_protocol{OSSIA::Minuit::create(ip, in_port, out_port)};
    });
}
}
