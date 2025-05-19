#pragma once

#pragma once

#ifdef KOTKOVDLL_EXPORTS
#define KOTKOVDLL_API __declspec(dllexport)
#else
#define KOTKOVDLL_API __declspec(dllimport)
#endif

#include "message.h"

struct MessageTransfer
{
	MessageHeader header = {};
	const wchar_t* data = nullptr;
	int clientID = 0;
};

extern "C"
{
	KOTKOVDLL_API MessageTransfer SendMsg(int to, int type = MT_DATA, const wchar_t* data = nullptr);
	KOTKOVDLL_API void FreeMessageTransfer(MessageTransfer msg);
}