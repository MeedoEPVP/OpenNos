﻿/*
 * This file is part of the OpenNos Emulator Project. See AUTHORS file for Copyright information
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 */

using OpenNos.ServiceRef.Internal.CommunicationServiceReference;
using System;
using System.ServiceModel;

namespace OpenNos.ServiceRef.Internal
{
    public class ServiceFactory
    {
        #region Members

        private static ServiceFactory _instance;
        private ICommunicationService _communicationServiceClient;
        private bool _disposed;
        private CommunicationCallback _instanceCallback;
        private InstanceContext _instanceContext;
        private bool _useMock;

        #endregion

        #region Instantiation

        public ServiceFactory()
        {
            // callback instance will be instantiated once per process
            _instanceCallback = new CommunicationCallback();
            _instanceContext = new InstanceContext(_instanceCallback);
            _useMock = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["UseMock"]);
        }

        #endregion

        #region Properties

        public static ServiceFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ServiceFactory();
                }

                return _instance;
            }
        }

        public CommunicationCallback CommunicationCallback
        {
            get
            {
                return _instanceCallback;
            }
        }

        public ICommunicationService CommunicationService
        {
            get
            {
                // reinitialize serviceclient which is faulted caused by an exception
                if (!_useMock && _communicationServiceClient != null && _communicationServiceClient is CommunicationServiceClient
                    && ((CommunicationServiceClient)_communicationServiceClient).State == CommunicationState.Faulted)
                {
                    _communicationServiceClient = new CommunicationServiceClient(_instanceContext);
                    return _communicationServiceClient;
                }

                if (_communicationServiceClient == null)
                {
                    if (!_useMock)
                    {
                        _communicationServiceClient = new CommunicationServiceClient(_instanceContext);
                    }
                    else
                    {
                        _communicationServiceClient = new FakeCommunicationService();
                    }
                }

                return _communicationServiceClient;
            }
        }

        #endregion

        #region Methods

        public void Dispose()
        {
            if (!_disposed)
            {
                Dispose(true);
                GC.SuppressFinalize(this);
                _disposed = true;
            }
        }

        public void Initialize()
        {
            if (!_useMock)
            {
                ((CommunicationServiceClient)CommunicationService).Open();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _instanceCallback.Dispose();
            }
        }

        #endregion
    }
}