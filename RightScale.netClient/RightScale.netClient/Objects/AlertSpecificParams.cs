using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightScale.netClient
{
    /// <summary>
    /// AlertSpecificParams define the parameters for controling AlertSpec behavior
    /// </summary>
    public class AlertSpecificParams
    {
        #region AlertSpecificParams Properties

        string decisionThresholdRegexValidator = @"^\d+$";

        /// <summary>
        /// Tag predicate for vote tags
        /// </summary>
        public string voters_tag_predicate { get; set; }

        string _decision_threshold;

        /// <summary>
        /// Decision threshold for AlertSpec
        /// </summary>
        public string decision_threshold
        {
            get
            {
                return _decision_threshold;
            }
            set
            {
                if (Utility.CheckStringRegex("decision_threshold", decisionThresholdRegexValidator, value))
                {
                    this._decision_threshold = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Decision_threshold cannot be set to " + value + ".  Regex validation for pattern [" + decisionThresholdRegexValidator + "] failed.");
                }
            }
        }

        #endregion

        #region AlertSpecificParams.ctor

        /// <summary>
        /// Fully parameterized constructor for AlertSpecificParams
        /// </summary>
        /// <param name="votersTagPredicate">Tag predicate for vote tags</param>
        /// <param name="decisionThreshold">Decision threshold for AlertSpec</param>
        public AlertSpecificParams(string votersTagPredicate, string decisionThreshold)
        {
            this.voters_tag_predicate = votersTagPredicate;
            this.decision_threshold = decisionThreshold;
        }

        /// <summary>
        /// Default constructor for AlertSpecificParams
        /// </summary>
        public AlertSpecificParams()
        {

        }

        #endregion
    }
}
