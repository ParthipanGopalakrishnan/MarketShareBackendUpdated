namespace MarketShare.Models.MarketShare
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="WHIAppSearchByConfig" />.
    /// </summary>
    public class WHIAppSearchByConfig
    {
        public string WHIAppConfigData { get; set; }

    }

    /// <summary>
    /// Defines the <see cref="WHIAppSearchByConfigDto" />.
    /// </summary>
    public class WHIAppSearchByConfigDto
    {        
        public IEnumerable<WHIAppSearchByConfig> SearchBy { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="WHIAppGridColumnsConfig" />.
    /// </summary>
    public class WHIAppGridColumnsConfig
    {
        public string WHIAppGridColumnsConfigData { get; set; }

    }

    /// <summary>
    /// Defines the <see cref="WHIAppGridColumnsConfigDto" />.
    /// </summary>
    public class WHIAppGridColumnsConfigDto
    {
        public IEnumerable<WHIAppGridColumnsConfig> gridColumns { get; set; }
    }
}
