﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.AppCommon.StaticValues
{
    public static class StaticValues
    {
        public const int GenreNameMaxLength  = 50;
        public const int GenreDescriptionMaxLength = 500;

        public const int MoviePosterMaxLength = 50;
        public const int MovieTitleMaxLength = 100;
        public const int MovieDescriptionMaxLength = 2000;
        public const int MoviePosterMaxFileSize = 1048576;

        public const int PersonnelNameMaxLength = 100;
        public const int PersonnelSurnameMaxLength = 100;
        public const int PersonnelNationalityMaxLength = 100;

        public const int MovieItemsLoaded = 20;
    }
}
