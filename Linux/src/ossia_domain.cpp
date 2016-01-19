#include "ossia_utils.hpp"

extern "C"
{
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

}
