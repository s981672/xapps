﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace xapps
{
	public class NetworkManager
	{
        INetworkManager iNetworkManager;

        public NetworkManager (INetworkManager service)
		{
            iNetworkManager = service;
		}

        public Task<MovieListData> requestMovieList()
        {
            return iNetworkManager.requestMovieList();
        }

        public Task <MovieDetailData> requestMovieDetail(String movieCd)
        {
            return iNetworkManager.requestMovieDetail(movieCd);
        }
	}
}
