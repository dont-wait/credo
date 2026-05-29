import Header from "@/components/layout/Header";
import { CheckCircle, XCircle, AlertTriangle, TrendingDown, TrendingUp, ArrowLeft } from "lucide-react";
import Link from "next/link";

// Mock data — static
const result = {
  id: "APP-2026-0048",
  name: "Nguyễn Văn An",
  cmnd: "079201012345",
  pdScore: 0.038,
  creditScore: 712,
  group: "B",
  decision: "APPROVED",
  approvedLimit: 120_000_000,
  interestRate: 13,
  maxTermMonths: 48,
  monthlyPayment: 3_204_000,
  collateralRequired: false,
  topFactors: [
    { label: "Lịch sử thanh toán tốt",              impact: +0.18, positive: true  },
    { label: "DTI thấp (23%)",                       impact: +0.14, positive: true  },
    { label: "Thời gian lịch sử tín dụng dài",       impact: +0.09, positive: true  },
    { label: "Credit utilization cao (68%)",          impact: -0.11, positive: false },
    { label: "Có 2 khoản vay mới trong 6 tháng",     impact: -0.06, positive: false },
  ],
  rejectionReasons: [] as string[],
};

const groupColors: Record<string,{color:string;bg:string}> = {
  A:{color:"#10b981",bg:"#064e3b"},  // Xuất sắc
  B:{color:"#3b82f6",bg:"#1e3a5f"},  // Tốt
  C:{color:"#f59e0b",bg:"#451a03"},  // Trung bình khá
  D:{color:"#f97316",bg:"#431407"},  // Trung bình (đổi màu để phân biệt với C)
  E:{color:"#ef4444",bg:"#450a0a"},  // Kém / Nợ xấu
};

function ScoreGauge({ score }: { score: number }) {
  const pct = (score - 150) / 600;  // đổi 300→150, 550→600
  const r = 80;
  const circ = Math.PI * r;
  const offset = circ * (1 - pct);
  const color = score >= 670 ? "#10b981"   // Nhóm A: 670–750
              : score >= 620 ? "#3b82f6"   // Nhóm B: 620–669
              : score >= 570 ? "#f59e0b"   // Nhóm C: 570–619
              : score >= 300 ? "#f97316"   // Nhóm D: 300–569
              : "#ef4444";                 // Nhóm E: 150–299

  return (
    <svg width={200} height={120} viewBox="0 0 200 120">
      <path d={`M 20 100 A 80 80 0 0 1 180 100`} fill="none" stroke="var(--border)" strokeWidth={14} strokeLinecap="round"/>
      <path d={`M 20 100 A 80 80 0 0 1 180 100`} fill="none" stroke={color} strokeWidth={14}
        strokeLinecap="round" strokeDasharray={circ} strokeDashoffset={offset}
        style={{ transition: "stroke-dashoffset 1s ease" }}/>
      <text x={100} y={88} textAnchor="middle" fill={color} fontSize={28} fontWeight={700} fontFamily="monospace">{score}</text>
      <text x={100} y={108} textAnchor="middle" fill="var(--text-muted)" fontSize={11}>/ 750</text>  {/* đổi 850→750 */}
      <text x={24}  y={116} fill="var(--text-muted)" fontSize={10}>150</text>  {/* đổi 300→150 */}
      <text x={172} y={116} fill="var(--text-muted)" fontSize={10}>750</text>  {/* đổi 850→750 */}
    </svg>
  );
}

export default function ScoringPage() {
  const g = groupColors[result.group];
  const approved = result.decision === "APPROVED";

  return (
    <div>
      <Header title={`Kết quả đánh giá — ${result.id}`} subtitle={result.name} />
      <div style={{ padding:24 }} className="animate-in">

        {/* Back */}
        <Link href="/applications" style={{ textDecoration:"none" }}>
          <div style={{ display:"inline-flex", alignItems:"center", gap:6, fontSize:13, color:"var(--text-muted)", marginBottom:20, cursor:"pointer" }}>
            <ArrowLeft size={14}/> Quay lại danh sách
          </div>
        </Link>

        <div style={{ display:"grid", gridTemplateColumns:"1fr 1fr", gap:16, marginBottom:16 }}>

          {/* Score Card */}
          <div style={{ background:"var(--bg-card)", border:"1px solid var(--border)", borderRadius:"var(--radius)", padding:24 }}>
            <div style={{ fontSize:14, fontWeight:600, marginBottom:4 }}>Credit Score</div>
            <div style={{ fontSize:12, color:"var(--text-muted)", marginBottom:16 }}>PD Score (MLP): {result.pdScore.toFixed(3)}</div>
            <div style={{ display:"flex", flexDirection:"column", alignItems:"center" }}>
              <ScoreGauge score={result.creditScore} />
              <span style={{ background:g.bg, color:g.color, padding:"4px 16px", borderRadius:20, fontSize:13, fontWeight:700, marginTop:8 }}>
                {({
                  A: "Nhóm A — Xuất sắc",
                  B: "Nhóm B — Tốt",
                  C: "Nhóm C — Trung bình khá",
                  D: "Nhóm D — Trung bình",
                  E: "Nhóm E — Kém / Nợ xấu",
                } as Record<string, string>)[result.group]}
              </span>
            </div>
          </div>

          {/* Decision */}
          <div style={{ background: approved?"var(--green-dim)":"var(--red-dim)", border:`1px solid ${approved?"var(--green)":"var(--red)"}`, borderRadius:"var(--radius)", padding:24 }}>
            <div style={{ display:"flex", alignItems:"center", gap:10, marginBottom:16 }}>
              {approved
                ? <CheckCircle size={24} color="var(--green)"/>
                : <XCircle    size={24} color="var(--red)"/>
              }
              <div style={{ fontSize:20, fontWeight:700, color: approved?"var(--green)":"var(--red)" }}>
                {approved ? "PHÊDUYỆT" : "TỪ CHỐI"}
              </div>
            </div>

            {approved ? (
              <div style={{ display:"grid", gridTemplateColumns:"1fr 1fr", gap:12 }}>
                {[
                  { label:"Hạn mức được duyệt",   value:`${(result.approvedLimit/1_000_000).toFixed(0)}tr VNĐ`, highlight:true },
                  { label:"Lãi suất",              value:`${result.interestRate}%/năm`, highlight:false },
                  { label:"Kỳ hạn tối đa",         value:`${result.maxTermMonths} tháng`, highlight:false },
                  { label:"Trả góp ước tính",      value:`${(result.monthlyPayment/1000).toFixed(0)}k/tháng`, highlight:false },
                  { label:"Tài sản thế chấp",      value: result.collateralRequired?"Yêu cầu":"Không yêu cầu", highlight:false },
                ].map(item => (
                  <div key={item.label} style={{ background:"rgba(0,0,0,0.2)", borderRadius:8, padding:"12px 14px" }}>
                    <div style={{ fontSize:11, color:"rgba(255,255,255,0.6)", marginBottom:4 }}>{item.label}</div>
                    <div style={{ fontSize:item.highlight?18:14, fontWeight:700, color:"#fff" }}>{item.value}</div>
                  </div>
                ))}
              </div>
            ) : (
              <div>
                {result.rejectionReasons.map((r,i) => (
                  <div key={i} style={{ display:"flex", gap:8, marginBottom:10, background:"rgba(0,0,0,0.2)", borderRadius:8, padding:"10px 12px" }}>
                    <AlertTriangle size={14} color="var(--red)" style={{ flexShrink:0, marginTop:2 }}/>
                    <span style={{ fontSize:13 }}>{r}</span>
                  </div>
                ))}
              </div>
            )}
          </div>
        </div>

        {/* SHAP Explanation */}
        <div style={{ background:"var(--bg-card)", border:"1px solid var(--border)", borderRadius:"var(--radius)", padding:24 }}>
          <div style={{ fontSize:14, fontWeight:600, marginBottom:4 }}>Giải thích quyết định (SHAP)</div>
          <div style={{ fontSize:12, color:"var(--text-muted)", marginBottom:20 }}>Top factors ảnh hưởng đến credit score — từ mô hình MLP</div>
          <div style={{ display:"flex", flexDirection:"column", gap:12 }}>
            {result.topFactors.map(f => {
              const barW = Math.abs(f.impact) / 0.2 * 100;
              return (
                <div key={f.label} style={{ display:"grid", gridTemplateColumns:"240px 1fr 60px", alignItems:"center", gap:16 }}>
                  <div style={{ fontSize:13 }}>{f.label}</div>
                  <div style={{ background:"var(--bg)", borderRadius:4, height:8, overflow:"hidden" }}>
                    <div style={{
                      width:`${barW}%`, height:"100%",
                      background: f.positive?"var(--green)":"var(--red)",
                      borderRadius:4, float: f.positive?"left":"right",
                    }}/>
                  </div>
                  <div style={{ display:"flex", alignItems:"center", gap:4, fontSize:13, fontWeight:600,
                    color: f.positive?"var(--green)":"var(--red)", justifyContent:"flex-end" }}>
                    {f.positive ? <TrendingUp size={12}/> : <TrendingDown size={12}/>}
                    {f.positive?"+":""}{f.impact.toFixed(2)}
                  </div>
                </div>
              );
            })}
          </div>
        </div>
      </div>
    </div>
  );
}