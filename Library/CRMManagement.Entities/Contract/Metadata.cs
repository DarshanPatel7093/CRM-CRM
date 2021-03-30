//-----------------------------------------------------------------------
// <copyright file="Metadata.cs" company="Premiere Digital Services">
//     Copyright Premiere Digital Services. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PDX.Entities.Contract
{
    using Nest;
    using PDX.Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class Metadata
    /// </summary>
    public class Metadata
    {
        protected DateTime? theatricalReleaseDate;

        /// <summary>
        /// Gets or sets the genres.
        /// </summary>
        /// <value>
        /// The genres.
        /// </value>
        [String(Name = "Genres", Index = FieldIndexOption.NotAnalyzed)]
        public string[] Genres
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.GenresString) ? new string[] { } : this.GenresString.Split(new[] { ',' }).Distinct().ToArray();
            }

            set
            {
                if (value != null && value.Length > 0)
                {
                    this.GenresString = string.Join(",", value.Select(v => "[" + v + "]"));
                }
            }
        }

        /// <summary>
        /// Gets or sets the genres string.
        /// </summary>
        /// <value>
        /// The genres string.
        /// </value>
        public string GenresString { private get; set; }

        /// <summary>
        /// Gets or sets the keyword string.
        /// </summary>
        /// <value>
        /// The keyword string.
        /// </value>
        [String(Name = "Keywords", Index = FieldIndexOption.NotAnalyzed)]
        public string[] Keywords
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.KeywordString) ? new string[] { } : this.KeywordString.Split(new[] { ',' }).Distinct().ToArray();
            }
            
            set
            {
                if (value != null && value.Length > 0)
                {
                    this.KeywordString = string.Join(",", value.Select(v => "[" + v + "]"));
                }
            }
        }

        /// <summary>
        /// Gets or sets the keywords.
        /// </summary>
        /// <value>
        /// The keywords.
        /// </value>
        public string KeywordString { private get; set; }

        /// <summary>
        /// Gets or sets the names.
        /// </summary>
        /// <value>
        /// The names.
        /// </value>
        [String(Name = "Names", Index = FieldIndexOption.NotAnalyzed)]
        public string[] Names
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.NamesString) ? new string[] { } : this.NamesString.Split(new[] { "],[" }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.TrimEnd(']').TrimStart('[')).Distinct().ToArray();
            }

            set
            {
                if (value != null && value.Length > 0)
                {
                    this.NamesString = string.Join(",", value.Select(v => "[" + v + "]"));
                }
            }
        }

        /// <summary>
        /// Gets or sets the names string.
        /// </summary>
        /// <value>
        /// The names string.
        /// </value>
        public string NamesString { private get; set; }

        /// <summary>
        /// Gets or sets the display name of the version.
        /// </summary>
        /// <value>
        /// The display name of the version.
        /// </value>
        [String(Name = "VersionDisplayName", Index = FieldIndexOption.NotAnalyzed)]
        public string[] VersionDisplayName
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.VersionDisplayNameString) ? new string[] { } : this.VersionDisplayNameString.Split(new[] { "],[" }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.TrimEnd(']').TrimStart('[')).ToArray();
            }

            set
            {
                if (value != null && value.Length > 0)
                {
                    this.VersionDisplayNameString = string.Join(",", value.Select(v => "[" + v + "]"));
                }
            }
        }

        /// <summary>
        /// Gets or sets the version display name string.
        /// </summary>
        /// <value>
        /// The version display name string.
        /// </value>
        public string VersionDisplayNameString { private get; set; }

        /// <summary>
        /// Gets or sets the theatrical release date.
        /// </summary>
        /// <value>
        /// The theatrical release date.
        /// </value>
        [Date(Name = "TheatricalReleaseDate")]
        public string TheatricalReleaseDate
        {
            get
            {
                return this.theatricalReleaseDate.HasValue ? this.theatricalReleaseDate.Value.ToString(Constant.DateFormat) : null;
            }

            set
            {
                DateTime date;
                DateTime.TryParse(value, out date);
                this.theatricalReleaseDate = date;
            }
        }

        /// <summary>
        /// Gets or sets the feature version identifier.
        /// </summary>
        /// <value>
        /// The feature version identifier.
        /// </value>
        public long? FeatureVersionId { protected get; set; }

        /// <summary>
        /// Gets or sets the season identifier.
        /// </summary>
        /// <value>
        /// The season identifier.
        /// </value>
        public long? SeasonId { protected get; set; }

        /// <summary>
        /// Gets or sets the episode identifier.
        /// </summary>
        /// <value>
        /// The episode identifier.
        /// </value>
        public long? EpisodeId { protected get; set; }

        /// <summary>
        /// Gets or sets the episode version identifier.
        /// </summary>
        /// <value>
        /// The episode version identifier.
        /// </value>
        public long? EpisodeVersionId { protected get; set; }

        /// <summary>
        /// Gets the feature version identifier.
        /// </summary>
        /// <returns>returns feature version id</returns>
        public long GetFeatureVersionId()
        {
            return this.FeatureVersionId.Value;
        }

        /// <summary>
        /// Gets the season identifier.
        /// </summary>
        /// <returns>returns Season Id</returns>
        public long GetSeasonId()
        {
            return this.SeasonId.Value;
        }

        /// <summary>
        /// Gets the episode identifier.
        /// </summary>
        /// <returns>returns Episode Id</returns>
        public long GetEpisodeId()
        {
            return this.EpisodeId.Value;
        }

        /// <summary>
        /// Gets the episode version identifier.
        /// </summary>
        /// <returns>returns Episode Version id</returns>
        public long GetEpisodeVersionId()
        {
            return this.EpisodeVersionId.Value;
        }
    }
}
