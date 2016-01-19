#include "ossia_utils.hpp"
extern "C"
{

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
