﻿using System.Threading.Tasks;
using System.Collections.Generic;

namespace xapps
{
	public interface INetworkManager
	{
        Task <MovieListData>requestMovieList();

        Task <MovieDetailData> requestMovieDetail(string movieCd);
	}
}
