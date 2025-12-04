using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// 常用工具类
/// </summary>
class Utils
{
    #region 取得app.config中参数值并建立缓存
    /// <summary>
    /// app.config参数字典
    /// </summary>
    private static Dictionary<string, string> _Parameters = new Dictionary<string, string>();
    /// <summary>
    /// 系统日志文件路径
    /// </summary>
    public static string SystemLogFile = GetParameterValue("SystemLogFile") ?? $"{Process.GetCurrentProcess().ProcessName}.Log";

    // 定义简繁对照字串
    const string SIMP = "皑蔼碍爱翱袄奥坝罢摆败颁办绊帮绑镑谤剥饱宝报鲍辈贝钡狈备惫绷笔毕毙闭边编贬变辩辫鳖瘪濒滨宾摈饼拨钵铂驳卜补参蚕残惭惨灿苍舱仓沧厕侧册测层诧搀掺蝉馋谗缠铲产阐颤场尝长偿肠厂畅钞车彻尘陈衬撑称惩诚骋痴迟驰耻齿炽冲虫宠畴踌筹绸丑橱厨锄雏础储触处传疮闯创锤纯绰辞词赐聪葱囱从丛凑窜错达带贷担单郸掸胆惮诞弹当挡党荡档捣岛祷导盗灯邓敌涤递缔点垫电淀钓调迭谍叠钉顶锭订东动栋冻斗犊独读赌镀锻断缎兑队对吨顿钝夺鹅额讹恶饿儿尔饵贰发罚阀珐矾钒烦范贩饭访纺飞废费纷坟奋愤粪丰枫锋风疯冯缝讽凤肤辐抚辅赋复负讣妇缚该钙盖干赶秆赣冈刚钢纲岗皋镐搁鸽阁铬个给龚宫巩贡钩沟构购够蛊顾剐关观馆惯贯广规硅归龟闺轨诡柜贵刽辊滚锅国过骇韩汉阂鹤贺横轰鸿红后壶护沪户哗华画划话怀坏欢环还缓换唤痪焕涣黄谎挥辉毁贿秽会烩汇讳诲绘荤浑伙获货祸击机积饥讥鸡绩缉极辑级挤几蓟剂济计记际继纪夹荚颊贾钾价驾歼监坚笺间艰缄茧检碱硷拣捡简俭减荐槛鉴践贱见键舰剑饯渐溅涧浆蒋桨奖讲酱胶浇骄娇搅铰矫侥脚饺缴绞轿较秸阶节茎惊经颈静镜径痉竞净纠厩旧驹举据锯惧剧鹃绢杰洁结诫届紧锦仅谨进晋烬尽劲荆觉决诀绝钧军骏开凯颗壳课垦恳抠库裤夸块侩宽矿旷况亏岿窥馈溃扩阔蜡腊莱来赖蓝栏拦篮阑兰澜谰揽览懒缆烂滥捞劳涝乐镭垒类泪篱离里鲤礼丽厉励砾历沥隶俩联莲连镰怜涟帘敛脸链恋炼练粮凉两辆谅疗辽镣猎临邻鳞凛赁龄铃凌灵岭领馏刘龙聋咙笼垄拢陇楼娄搂篓芦卢颅庐炉掳卤虏鲁赂禄录陆驴吕铝侣屡缕虑滤绿峦挛孪滦乱抡轮伦仑沦纶论萝罗逻锣箩骡骆络妈玛码蚂马骂吗买麦卖迈脉瞒馒蛮满谩猫锚铆贸么霉没镁门闷们锰梦谜弥觅绵缅庙灭悯闽鸣铭谬谋亩钠纳难挠脑恼闹馁腻撵捻酿鸟聂啮镊镍柠狞宁拧泞钮纽脓浓农疟诺欧鸥殴呕沤盘庞国爱赔喷鹏骗飘频贫苹凭评泼颇扑铺朴谱脐齐骑岂启气弃讫牵扦钎铅迁签谦钱钳潜浅谴堑枪呛墙蔷强抢锹桥乔侨翘窍窃钦亲轻氢倾顷请庆琼穷趋区躯驱龋颧权劝却鹊让饶扰绕热韧认纫荣绒软锐闰润洒萨鳃赛伞丧骚扫涩杀纱筛晒闪陕赡缮伤赏烧绍赊摄慑设绅审婶肾渗声绳胜圣师狮湿诗尸时蚀实识驶势释饰视试寿兽枢输书赎属术树竖数帅双谁税顺说硕烁丝饲耸怂颂讼诵擞苏诉肃虽绥岁孙损笋缩琐锁獭挞抬摊贪瘫滩坛谭谈叹汤烫涛绦腾誊锑题体屉条贴铁厅听烃铜统头图涂团颓蜕脱鸵驮驼椭洼袜弯湾顽万网韦违围为潍维苇伟伪纬谓卫温闻纹稳问瓮挝蜗涡窝呜钨乌诬无芜吴坞雾务误锡牺袭习铣戏细虾辖峡侠狭厦锨鲜纤咸贤衔闲显险现献县馅羡宪线厢镶乡详响项萧销晓啸蝎协挟携胁谐写泻谢锌衅兴汹锈绣虚嘘须许绪续轩悬选癣绚学勋询寻驯训讯逊压鸦鸭哑亚讶阉烟盐严颜阎艳厌砚彦谚验鸯杨扬疡阳痒养样瑶摇尧遥窑谣药爷页业叶医铱颐遗仪彝蚁艺亿忆义诣议谊译异绎荫阴银饮樱婴鹰应缨莹萤营荧蝇颖哟拥佣痈踊咏涌优忧邮铀犹游诱舆鱼渔娱与屿语吁御狱誉预驭鸳渊辕园员圆缘远愿约跃钥岳粤悦阅云郧匀陨运蕴酝晕韵杂灾载攒暂赞赃脏凿枣灶责择则泽贼赠扎札轧铡闸诈斋债毡盏斩辗崭栈战绽张涨帐账胀赵蛰辙锗这贞针侦诊镇阵挣睁狰帧郑证织职执纸挚掷帜质钟终种肿众诌轴皱昼骤猪诸诛烛瞩嘱贮铸筑驻专砖转赚桩庄装妆壮状锥赘坠缀谆浊兹资渍踪综总纵邹诅组钻致钟么为只凶准启板里雳余链泄";
    const string TRAD = "皚藹礙愛翺襖奧壩罷擺敗頒辦絆幫綁鎊謗剝飽寶報鮑輩貝鋇狽備憊繃筆畢斃閉邊編貶變辯辮鼈癟瀕濱賓擯餅撥缽鉑駁蔔補參蠶殘慚慘燦蒼艙倉滄廁側冊測層詫攙摻蟬饞讒纏鏟産闡顫場嘗長償腸廠暢鈔車徹塵陳襯撐稱懲誠騁癡遲馳恥齒熾沖蟲寵疇躊籌綢醜櫥廚鋤雛礎儲觸處傳瘡闖創錘純綽辭詞賜聰蔥囪從叢湊竄錯達帶貸擔單鄲撣膽憚誕彈當擋黨蕩檔搗島禱導盜燈鄧敵滌遞締點墊電澱釣調叠諜疊釘頂錠訂東動棟凍鬥犢獨讀賭鍍鍛斷緞兌隊對噸頓鈍奪鵝額訛惡餓兒爾餌貳發罰閥琺礬釩煩範販飯訪紡飛廢費紛墳奮憤糞豐楓鋒風瘋馮縫諷鳳膚輻撫輔賦複負訃婦縛該鈣蓋幹趕稈贛岡剛鋼綱崗臯鎬擱鴿閣鉻個給龔宮鞏貢鈎溝構購夠蠱顧剮關觀館慣貫廣規矽歸龜閨軌詭櫃貴劊輥滾鍋國過駭韓漢閡鶴賀橫轟鴻紅後壺護滬戶嘩華畫劃話懷壞歡環還緩換喚瘓煥渙黃謊揮輝毀賄穢會燴彙諱誨繪葷渾夥獲貨禍擊機積饑譏雞績緝極輯級擠幾薊劑濟計記際繼紀夾莢頰賈鉀價駕殲監堅箋間艱緘繭檢堿鹼揀撿簡儉減薦檻鑒踐賤見鍵艦劍餞漸濺澗漿蔣槳獎講醬膠澆驕嬌攪鉸矯僥腳餃繳絞轎較稭階節莖驚經頸靜鏡徑痙競淨糾廄舊駒舉據鋸懼劇鵑絹傑潔結誡屆緊錦僅謹進晉燼盡勁荊覺決訣絕鈞軍駿開凱顆殼課墾懇摳庫褲誇塊儈寬礦曠況虧巋窺饋潰擴闊蠟臘萊來賴藍欄攔籃闌蘭瀾讕攬覽懶纜爛濫撈勞澇樂鐳壘類淚籬離裏鯉禮麗厲勵礫曆瀝隸倆聯蓮連鐮憐漣簾斂臉鏈戀煉練糧涼兩輛諒療遼鐐獵臨鄰鱗凜賃齡鈴淩靈嶺領餾劉龍聾嚨籠壟攏隴樓婁摟簍蘆盧顱廬爐擄鹵虜魯賂祿錄陸驢呂鋁侶屢縷慮濾綠巒攣孿灤亂掄輪倫侖淪綸論蘿羅邏鑼籮騾駱絡媽瑪碼螞馬罵嗎買麥賣邁脈瞞饅蠻滿謾貓錨鉚貿麽黴沒鎂門悶們錳夢謎彌覓綿緬廟滅憫閩鳴銘謬謀畝鈉納難撓腦惱鬧餒膩攆撚釀鳥聶齧鑷鎳檸獰甯擰濘鈕紐膿濃農瘧諾歐鷗毆嘔漚盤龐國愛賠噴鵬騙飄頻貧蘋憑評潑頗撲鋪樸譜臍齊騎豈啓氣棄訖牽扡釺鉛遷簽謙錢鉗潛淺譴塹槍嗆牆薔強搶鍬橋喬僑翹竅竊欽親輕氫傾頃請慶瓊窮趨區軀驅齲顴權勸卻鵲讓饒擾繞熱韌認紉榮絨軟銳閏潤灑薩鰓賽傘喪騷掃澀殺紗篩曬閃陝贍繕傷賞燒紹賒攝懾設紳審嬸腎滲聲繩勝聖師獅濕詩屍時蝕實識駛勢釋飾視試壽獸樞輸書贖屬術樹豎數帥雙誰稅順說碩爍絲飼聳慫頌訟誦擻蘇訴肅雖綏歲孫損筍縮瑣鎖獺撻擡攤貪癱灘壇譚談歎湯燙濤縧騰謄銻題體屜條貼鐵廳聽烴銅統頭圖塗團頹蛻脫鴕馱駝橢窪襪彎灣頑萬網韋違圍爲濰維葦偉僞緯謂衛溫聞紋穩問甕撾蝸渦窩嗚鎢烏誣無蕪吳塢霧務誤錫犧襲習銑戲細蝦轄峽俠狹廈鍁鮮纖鹹賢銜閑顯險現獻縣餡羨憲線廂鑲鄉詳響項蕭銷曉嘯蠍協挾攜脅諧寫瀉謝鋅釁興洶鏽繡虛噓須許緒續軒懸選癬絢學勳詢尋馴訓訊遜壓鴉鴨啞亞訝閹煙鹽嚴顔閻豔厭硯彥諺驗鴦楊揚瘍陽癢養樣瑤搖堯遙窯謠藥爺頁業葉醫銥頤遺儀彜蟻藝億憶義詣議誼譯異繹蔭陰銀飲櫻嬰鷹應纓瑩螢營熒蠅穎喲擁傭癰踴詠湧優憂郵鈾猶遊誘輿魚漁娛與嶼語籲禦獄譽預馭鴛淵轅園員圓緣遠願約躍鑰嶽粵悅閱雲鄖勻隕運蘊醞暈韻雜災載攢暫贊贓髒鑿棗竈責擇則澤賊贈紮劄軋鍘閘詐齋債氈盞斬輾嶄棧戰綻張漲帳賬脹趙蟄轍鍺這貞針偵診鎮陣掙睜猙幀鄭證織職執紙摯擲幟質鍾終種腫衆謅軸皺晝驟豬諸誅燭矚囑貯鑄築駐專磚轉賺樁莊裝妝壯狀錐贅墜綴諄濁茲資漬蹤綜總縱鄒詛組鑽緻鐘麼為隻兇準啟闆裡靂餘鍊洩";

    /// <summary>
    /// 环境参数写入字典，如果存在则更新
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    public static void SetParameterValue(string key, string value)
    {
        if (key == null || (key = key.Trim()).Length == 0) return;
        if (value == null || (value = value.Trim()).Length == 0) return;
        if (_Parameters.ContainsKey(key)) _Parameters[key] = value;
        else _Parameters.Add(key, value);
    }

    /// <summary>
    /// 取得app.config中参数值
    /// </summary>
    /// <param name="key">参数名</param>
    /// <returns>返回参数的值，若无此参数返回null</returns>
    public static string GetParameterValue(string key)
    {
        string result = null;
        if (key != null || !"".Equals(key.Trim()))
        {
            key = key.Trim();
            if (_Parameters.ContainsKey(key))
            {   //包含参数，直接返回值
                result = _Parameters[key];
            }
            else
            {   //不包含参数，尝试从app.config中读参
                result = ConfigurationManager.AppSettings[key];
                //如果有读到参数，记录到字典以便下次使用
                if (result != null) _Parameters.Add(key, result);
            }
        }
        return result;
    }
    #endregion

    #region MD5加密，依赖：using System.Security.Cryptography;
    public static string GetStrMd5(string ConvertString)
    {
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        string result = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)));
        result = result.Replace("-", "");
        return result;
    }
    #endregion

    #region BASE64加/解密
    /// <summary>
    /// 将字符串转换为Base64字串
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string Base64Encrypt(string str)
    {
        byte[] buff = Encoding.UTF8.GetBytes(str);
        return Convert.ToBase64String(buff);
    }

    /// <summary>
    /// 将BASE64字串解码为UTF8正常字串
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string Base64Decrypt(string str)
    {
        byte[] buff = Convert.FromBase64String(str);
        return Encoding.UTF8.GetString(buff);
    }
    #endregion

    /// <summary>
    /// 测试IP是否能Ping通
    /// </summary>
    /// <param name="ip">IP地址</param>
    /// <returns></returns>
    public static bool IsIpOnline(string ip)
    {
        Ping pingSender = new Ping();
        //第一个参数为ip地址，第二个参数为ping的timeout时间
        PingReply reply = pingSender.Send(ip, 1000);
        return reply.Status == IPStatus.Success;
    }

    #region 拼接字串方法GetJoinStr():用指定分隔符拼接字串，GetSqlInStr():拼接SQL中IN条件的条件字串
    /// <summary>
    /// 拼接SQL中IN条件的条件字串
    /// </summary>
    /// <param name="list">可迭代对象(数组，列表，集合等)</param>
    /// <returns>返回SQL WHERE IN条件的字串，(EX:'s1','s2','s3','s4')</returns>
    public static string GetSqlInStr(IEnumerable<string> list)
    {
        string result = "";
        if (list.Count() == 0) return result;
        foreach (string s in list)
        {
            result += $"'{s}',";
        }
        result = result.TrimEnd(',');
        return result;
    }

    /// <summary>
    /// 使用指定分隔符拼接字串
    /// </summary>
    /// <param name="list">可迭代对象(数组，列表，集合等)</param>
    /// <returns>返回将对象所有元素用分隔符拼接起来的长字串,对象为空返回空字串</returns>
    public static string GetJoinStr(IEnumerable<string> list, char splitChar)
    {
        string result = "";
        if (list.Count() == 0) return result;
        foreach (string s in list)
        {
            result += s + splitChar;
        }
        result = result.TrimEnd(splitChar);
        return result;
    }
    #endregion

    #region 判断进程是否已存在
    /// <summary>
    /// 判断进程是否已存在(放在FormLoad事件中使用)
    /// </summary>
    /// <param name="processName">进程名，当前进程名取得方式(Process.GetCurrentProcess().ProcessName)</param>
    /// <returns>True:进程已存在，False:进程不存在或进程名为空</returns>
    public static bool IsProcessExist(string processName)
    {
        int result = 0;
        //进程名无效时返回False
        if (processName == null || "".Equals(processName.Trim())) return false;
        processName = processName.Trim();
        try
        {
            //获取当前进程数组
            Process[] processes = Process.GetProcesses();
            foreach (Process p in processes)
            {
                if (p.ProcessName.Equals(processName))
                {   //进程名相同，返回结果
                    result++;
                    if (result > 1) break;  //进程超过1个
                }
            }
        }
        catch (Exception ex)
        {
            AppendToFile(SystemLogFile, $"Utils.ProcessExist({processName}) fail:{ex.Message}", true);
        }
        return result > 1;
    }
    #endregion

    #region 检查参数是否未定义或定义为空
    /// <summary>
    /// 检查参数是否未定义或定义为空
    /// </summary>
    /// <param name="lPara">参数key列表(app.config中的参数key)</param>
    /// <returns>True:参数未定义或为空值，False:参数有定义</returns>
    public static bool ParaNotExist(List<string> lPara)
    {
        if (lPara == null) return false;
        int iErrorCnt = 0;
        string sVal;
        foreach (string sPara in lPara)
        {
            sVal = Utils.GetParameterValue(sPara);
            if (sVal == null || sVal.Trim().Length == 0)
            {
                AppendToFile(SystemLogFile, $"Utils.ParaNotExist，系统参数[{sPara}]未设定或为空.", true);
                iErrorCnt++;
            }
        }
        return iErrorCnt > 0;
    }
    #endregion

    #region 将对象转换为定长字符串返回，长度不足补满
    /// <summary>
    /// 将对象转换为定长的字符串，不足用空格左补位
    /// </summary>
    /// <param name="obj">转换对象</param>
    /// <param name="iLength">结果字串长度</param>
    /// <returns>定长的字符串，传入对象为空时，转出定长补位字串</returns>
    public static string GetPadString(object obj, int iLength)
    {
        return GetPadString(obj, iLength, false, ' ');
    }

    /// <summary>
    /// 将对象转换为定长的字符串，不足用空格补位
    /// </summary>
    /// <param name="obj">转换对象</param>
    /// <param name="iLength">结果字串长度</param>
    /// <param name="bPadRight">True:右补位，False:左补位</param>
    /// <returns>定长的字符串，传入对象为空时，转出定长补位字串</returns>
    public static string GetPadString(object obj, int iLength, bool bPadRight)
    {
        return GetPadString(obj, iLength, bPadRight, ' ');
    }

    /// <summary>
    /// 将对象转换为定长的字符串，不足则左补位
    /// </summary>
    /// <param name="obj">转换对象</param>
    /// <param name="iLength">结果字串长度</param>
    /// <param name="cFillChar">字符串长度不足时，补位的字符</param>
    /// <returns>定长的字符串，传入对象为空时，转出定长补位字串</returns>
    public static string GetPadString(object obj, int iLength, char cFillChar)
    {
        return GetPadString(obj, iLength, false, cFillChar);
    }

    /// <summary>
    /// 将对象转换为定长的字符串
    /// </summary>
    /// <param name="obj">转换对象</param>
    /// <param name="iLength">结果字串长度</param>
    /// <param name="bPadRight">True:右补位，False:左补位</param>
    /// <param name="cFillChar">字符串长度不足时，补位的字符</param>
    /// <returns>定长的字符串，传入对象为空时，转出定长补位字串</returns>
    public static string GetPadString(object obj, int iLength, bool bPadRight, char cFillChar)
    {
        string result = "";
        try
        {
            result = obj == null ? "" : Convert.ToString(obj).Trim();
            if (result.Length > iLength)
            {
                //字符超出长度，PadRight从右边截取，PadLeft从左边截取
                result = bPadRight ? result.Substring(result.Length - iLength, iLength) : result.Substring(0, iLength);
            }
            else if (result.Length < iLength)
            {
                result = bPadRight ? result.PadRight(iLength, cFillChar) : result.PadLeft(iLength, cFillChar);
            }
        }
        catch (Exception ex)
        {
            AppendToFile(SystemLogFile, "Utils.GetPadString执行错误，" + ex.Message, true);
        }
        return result;
    }
    #endregion
    #region 字串简繁转换，引用：Microsoft.VisualBasic，依赖：Microsoft.VisualBasic;
    /// <summary>
    /// 字符串简体转繁体
    /// </summary>
    /// <param name="strSimple"></param>
    /// <returns></returns>
    public static string ToTraditionalChinese(string strSimple)
    {
        string strTraditional = Strings.StrConv(strSimple, VbStrConv.TraditionalChinese, 0);
        return strTraditional;
    }
    /// <summary>
    /// 字符串繁体转简体
    /// </summary>
    /// <param name="strTraditional"></param>
    /// <returns></returns>
    public static string ToSimplifiedChinese(string strTraditional)
    {
        string strSimple = Strings.StrConv(strTraditional, VbStrConv.SimplifiedChinese, 0);
        return strSimple;
    }
    #endregion

    #region 写字串至本地TXT文件，依赖：using System.IO
    /// <summary>
    /// 将内容追加到文件中
    /// </summary>
    /// <param name="filepath">文件路径及文件名称</param>
    /// <param name="content">写入内容</param>
    /// <param name="encoding">文件编码</param>
    /// <param name="needtime">True:在内容前加入系统时间，False:不加入系统时间</param>
    /// <returns>成功返回True，失败返回False</returns>
    private static bool AppendToFile(string filepath, List<string> content, Encoding encoding, bool marktime)
    {
        if (content == null || content.Count == 0 || string.IsNullOrWhiteSpace(filepath))
            return false;

        try
        {
            // 使用静态锁防止多线程同时访问
            lock (AppendToFileLock)
            {
                string fullLogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filepath);
                string fileName = $"{DateTime.Now:yyyyMMdd}.log";
                string fullPath = Path.Combine(fullLogPath, fileName);

                // 确保目录存在
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                // 处理时间标记
                if (marktime)
                {
                    content[0] = $"{DateTime.Now:yyyy/MM/dd HH:mm:ss} {content[0]}";
                }

                bool isArchiving = false;

                // 安全的文件长度检查（避免文件锁定）
                if (File.Exists(fullPath))
                {
                    using (var fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        // 在共享读取模式下检查长度
                        if (fs.Length > 2097152)
                        {
                            isArchiving = true;
                        }
                    }
                }

                // 处理文件归档
                if (isArchiving)
                {
                    string archivePath = Path.Combine(
                        Path.GetDirectoryName(fullPath),
                        $"{Path.GetFileNameWithoutExtension(fullPath)}-{DateTime.Now:yyMMddHHmmss}{Path.GetExtension(fullPath)}"
                    );

                    // 确保所有句柄关闭后再移动
                    File.Move(fullPath, archivePath);
                }

                // 安全写入（使用FileStream控制共享模式）
                using (var fs = new FileStream(fullPath, FileMode.Append, FileAccess.Write, FileShare.Read))
                using (var writer = new StreamWriter(fs, encoding))
                {
                    foreach (string line in content)
                    {
                        writer.WriteLine(line);
                    }
                }

                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss} Utils.AppendToFile fail: {ex.Message}");
            return false;
        }
    }

    // 添加静态锁对象
    private static readonly object AppendToFileLock = new object();

    /// <summary>
    /// 将内容追加到文件中(Encoding.UTF8)
    /// </summary>
    /// <param name="filepath">文件路径及文件名称</param>
    /// <param name="content">写入内容</param>
    /// <param name="marktime">True:在内容前加入系统时间，False:不加入系统时间</param>
    /// <returns>成功返回True，失败返回False</returns>
    public static bool AppendToFile(string filepath, List<string> content, bool marktime)
    {
        return AppendToFile(filepath, content, Encoding.UTF8, marktime);
    }

    /// <summary>
    /// 将内容追加到文件中(Encoding.UTF8)
    /// </summary>
    /// <param name="filepath">文件路径及文件名称</param>
    /// <param name="content">写入内容</param>
    /// <param name="marktime">True:在内容前加入系统时间，False:不加入系统时间</param>
    /// <returns>成功返回True，失败返回False</returns>
    public static bool AppendToFile(string filepath, string content, bool marktime)
    {
        return AppendToFile(filepath, new List<string>() { content }, marktime);
    }

    /// <summary>
    /// 将内容追加到文件中(Encoding.UTF8)
    /// </summary>
    /// <param name="filepath">文件路径及文件名称</param>
    /// <param name="content">写入内容</param>
    /// <param name="encoding">指定写入内容的编码(System.Text.Encoding)</param>
    /// <returns>成功返回True，失败返回False</returns>
    public static bool AppendToFile(string filepath, string content, Encoding encoding)
    {
        return AppendToFile(filepath, new List<string>() { content }, encoding, false);
    }
    #endregion

    #region 取连接字串及参数
    /// <summary>
    /// 取DB连接字串
    /// 依赖配置文件项："W3WebApiUrl": "https://cloud.longchengreentech.com:8443/",
    /// </summary>
    /// <param name="dbName">资料库名</param>
    /// <param name="connStr">连接字串结果</param>
    /// <returns>null为失败</returns>
    public static string GetDbConnectionString(string dbName)
    {
        string res = null;

        if (!string.IsNullOrWhiteSpace(dbName))
        {
            try
            {
                string url = GetParameterValue("W3WebApiUrl") + "Utils/GetDBConnectionString?dbname=" + dbName;
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage resp = client.PostAsync(url, null).Result;
                    string result = resp.Content.ReadAsStringAsync().Result;
                    if (resp.StatusCode == System.Net.HttpStatusCode.OK && result.Contains("success") && result.Contains("data"))
                    {
                        HttpResponseInfo jres = JsonConvert.DeserializeObject<HttpResponseInfo>(result);
                        res = jres.success ? jres.data.ToString() : null;
                    }
                }
            }
            catch (Exception ex)
            {
                AppendToFile(SystemLogFile, $"GetDBConnectionString('{dbName}')执行失败，错误信息是："+ex.Message,true);
            }
        }
        else
        {
            AppendToFile(SystemLogFile, $"GetDBConnectionString('{dbName}')，参数无效", true);
        }
        return res;
    }

    /// <summary>
    /// 取DB连接字串
    /// 依赖配置文件项："W3WebApiUrl": "https://cloud.longchengreentech.com:8443/",
    /// </summary>
    /// <param name="dbName">资料库名</param>
    /// <param name="connStr">连接字串结果</param>
    /// <returns>true:成功 / false:失败</returns>
    public static bool GetDbConnectionString(string dbName, out string connStr)
    {
        bool success = false;
        connStr = string.Empty;

        if (!string.IsNullOrWhiteSpace(dbName))
        {
            try
            {
                string url = GetParameterValue("W3WebApiUrl") + "Utils/GetDBConnectionString?dbname=" + dbName;
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage resp = client.PostAsync(url, null).Result;
                    string result = resp.Content.ReadAsStringAsync().Result;
                    if (resp.StatusCode == System.Net.HttpStatusCode.OK && result.Contains("success") && result.Contains("data"))
                    {
                        HttpResponseInfo jres = JsonConvert.DeserializeObject<HttpResponseInfo>(result);
                        success = jres.success;
                        connStr = jres.data.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                AppendToFile(SystemLogFile, $"GetDBConnectionString('{dbName}')执行失败，错误信息是：" + ex.Message, true);
            }
        }
        else
        {
            AppendToFile(SystemLogFile, $"GetDBConnectionString('{dbName}')，参数无效", true);
        }
        return success;
    }

    /// <summary>
    /// 取全局参数值
    /// 依赖配置文件项："W3WebApiUrl": "https://cloud.longchengreentech.com:8443/",
    /// </summary>
    /// <param name="key">参数key</param>
    /// <param name="value">返回参数key对应值</param>
    /// <returns>true:成功 / false:失败</returns>
    public static bool GetGlobalParameter(string key, out string value)
    {
        bool success = false;
        value = string.Empty;

        if (!string.IsNullOrWhiteSpace(key))
        {
            try
            {
                string url = GetParameterValue("W3WebApiUrl") + "Utils/GetGlobalParameter?key=" + key;
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage resp = client.PostAsync(url, null).Result;
                    string result = resp.Content.ReadAsStringAsync().Result;
                    if (resp.StatusCode == System.Net.HttpStatusCode.OK && result.Contains("success") && result.Contains("data"))
                    {
                        HttpResponseInfo jres = JsonConvert.DeserializeObject<HttpResponseInfo>(result);
                        success = jres.success;
                        value = jres.data.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                AppendToFile(SystemLogFile, $"GetGlobalParameter('{key}')执行失败，错误信息是："+ex.Message, true);
            }
        }
        else
        {
            AppendToFile(SystemLogFile, $"GetGlobalParameter('{key}')，参数无效",true);
        }
        return success;
    }
    /// <summary>
    /// WebApi返回结果构造
    /// </summary>
    struct HttpResponseInfo
    {
        public bool success { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
    #endregion

    /// <summary>
    /// 取得日期对应的中文星期几
    /// </summary>
    /// <param name="date">日期</param>
    /// <returns></returns>
    public static string GetWeekDay(DateTime date)
    {
        string[] weekDays = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
        string result = weekDays[Convert.ToInt32(date.DayOfWeek.ToString("d"))].ToString();
        return result;
    }
}