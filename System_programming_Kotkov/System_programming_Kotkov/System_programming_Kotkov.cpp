#include <iostream>
#include <conio.h>
#include <Windows.h>
#include "Session.h"

DWORD WINAPI MyThread(LPVOID lpParameter)
{
	auto session = static_cast<Session*>(lpParameter);
	SafeWrite("session", session->sessionID, "created");
	while (true)
	{
		Message m;
		if (session->getMessage(m))
		{
			switch (m.header.messageType)
			{
			case MT_CLOSE:
			{
				SafeWrite("session", session->sessionID, "closed");
				delete session;
				return 0;
			}
			}
		}
	}
	return 0;
}


int main()
{
	std::vector<Session*> sessions;
	int i = 0;

	HANDLE hStartEvent = CreateEvent(NULL, FALSE, FALSE, L"StartEvent");
	HANDLE hStopEvent = CreateEvent(NULL, FALSE, FALSE, L"StopEvent");
	HANDLE hConfirmEvent = CreateEvent(NULL, FALSE, FALSE, L"ConfirmEvent");
	HANDLE hExitEvent = CreateEvent(NULL, FALSE, FALSE, L"ExitEvent");
	HANDLE hControlEvents[3] = { hStartEvent, hStopEvent, hExitEvent };

	while (i >= 0){
		int n = WaitForMultipleObjects(3, hControlEvents, FALSE, INFINITE) - WAIT_OBJECT_0;
		switch (n)
		{
		case 0:
		{
			sessions.push_back(new Session(i++));
			CloseHandle(CreateThread(NULL, 0, MyThread, (LPVOID)sessions.back(), 0, NULL));
			SetEvent(hConfirmEvent);
			break;
		}
		case 1:
		{
			if (i == 0) {
				SetEvent(hExitEvent);
				break;
			}
			sessions.back()->addMessage(MT_CLOSE);
			sessions.pop_back();
			SetEvent(hConfirmEvent);
			i--;
			break;
		}
		case 2:
		{
			sessions.clear();
			SetEvent(hConfirmEvent);
			break;
		}
			
		}
	} 
	SetEvent(hConfirmEvent);


	return 0;
}



