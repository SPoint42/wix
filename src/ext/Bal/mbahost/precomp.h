#pragma once
// Copyright (c) .NET Foundation and contributors. All rights reserved. Licensed under the Microsoft Reciprocal License. See LICENSE.TXT file in the project root for full license information.


#include <windows.h>
#include <msiquery.h>
#include <metahost.h>
#include <shlwapi.h>

#import <mscorlib.tlb> raw_interfaces_only rename("ReportEvent", "mscorlib_ReportEvent")

#include <dutil.h>
#include <osutil.h>
#include <pathutil.h>
#include <regutil.h>
#include <strutil.h>
#include <xmlutil.h>

#include <BootstrapperEngine.h>
#include <BootstrapperApplication.h>
#include <IBootstrapperEngine.h>
#include <IBootstrapperApplication.h>
#include <IBootstrapperApplicationFactory.h>

#include <balutil.h>

#include <preqba.h>
#include <WixToolset.Mba.Host.h> // includes the generated assembly name macros.

#include "mbahost.h"
