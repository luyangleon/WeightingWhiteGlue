using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightingWhiteGlue.model
{
    public class WeighingRecord
    {
        /// <summary>
        /// 称重Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 厂区
        /// </summary>
        public string Plant { get; set; }
        /// <summary>
        /// 机台代号
        /// </summary>
        public string MachineId { get; set; }
        /// <summary>
        /// 班别
        /// </summary>
        public string Shift { get; set; }
        /// <summary>
        /// 称重类型
        /// </summary>
        public string WeighingType { get; set; }
        /// <summary>
        /// 注水量
        /// </summary>
        public decimal? WaterRate { get; set; }
        /// <summary>
        /// 开始称重重量
        /// </summary>
        public decimal? WeighingWeightBegin { get; set; }
        /// <summary>
        /// 结束称重重量
        /// </summary>
        public decimal? WeighingWeightEnd { get; set; }
        /// <summary>
        /// 开始称重时间
        /// </summary>
        public DateTime? WeighingTimeBegin { get; set; }
        /// <summary>
        /// 结束称重时间
        /// </summary>
        public DateTime? WeighingTimeEnd { get; set; }
    }
}
