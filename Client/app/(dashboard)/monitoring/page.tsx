"use client";
import Header from "@/components/layout/Header";
import { TrendingUp, TrendingDown, AlertTriangle, RefreshCw, Activity } from "lucide-react";

const months = ["T11","T12","T1","T2","T3","T4","T5"];
const giniData  = [0.51, 0.53, 0.54, 0.56, 0.57, 0.58, 0.58];
const ksData    = [0.35, 0.37, 0.38, 0.39, 0.40, 0.41, 0.41];
const psiData   = [0.04, 0.05, 0.06, 0.07, 0.07, 0.08, 0.08];

const metrics = [
  { label:"Gini Coefficient", value:"0.58", prev:"0.57", good:true,  threshold:"≥ 0.55", unit:"" },
  { label:"KS Statistic",     value:"0.41", prev:"0.40", good:true,  threshold:"≥ 0.35", unit:"" },
  { label:"AUC-ROC",          value:"0.79", prev:"0.78", good:true,  threshold:"≥ 0.72", unit:"" },
  { label:"PSI (tháng này)",  value:"0.08", prev:"0.07", good:true,  threshold:"< 0.10", unit:"" },
];

const vintageData = [
  { cohort:"T11/2025", size:1240, default3m:"3.2%", default6m:"5.8%", default12m:"8.1%" },
  { cohort:"T12/2025", size:1380, default3m:"3.0%", default6m:"5.4%", default12m:"—"    },
  { cohort:"T1/2026",  size:1150, default3m:"2.8%", default6m:"4.9%", default12m:"—"    },
  { cohort:"T2/2026",  size:1290, default3m:"3.1%", default6m:"—",    default12m:"—"    },
  { cohort:"T3/2026",  size:1420, default3m:"2.9%", default6m:"—",    default12m:"—"    },
];

function MiniChart({ data, color, height=60 }: { data:number[]; color:string; height?:number }) {
  const min = Math.min(...data) * 0.95;
  const max = Math.max(...data) * 1.02;
  const w = 280, h = height;
  const pts = data.map((v,i) => {
    const x = (i / (data.length-1)) * w;
    const y = h - ((v-min)/(max-min)) * h;
    return `${x},${y}`;
  }).join(" ");
  return (
    <svg width={w} height={h} viewBox={`0 0 ${w} ${h}`} style={{ overflow:"visible" }}>
      <polyline points={pts} fill="none" stroke={color} strokeWidth={2} strokeLinejoin="round"/>
      {data.map((v,i) => {
        const x = (i / (data.length-1)) * w;
        const y = h - ((v-min)/(max-min)) * h;
        return <circle key={i} cx={x} cy={y} r={3} fill={color}/>;
      })}
    </svg>
  );
}

export default function MonitoringPage() {
  return (
    <div>
      <Header title="Model Monitoring" subtitle="Theo dõi hiệu năng MLP model theo thời gian" />
      <div style={{ padding:24 }} className="animate-in">

        {/* Model status banner */}
        <div style={{
          display:"flex", alignItems:"center", justifyContent:"space-between",
          background:"var(--green-dim)", border:"1px solid rgba(16,185,129,0.3)",
          borderRadius:"var(--radius)", padding:"14px 20px", marginBottom:20,
        }}>
          <div style={{ display:"flex", alignItems:"center", gap:10 }}>
            <Activity size={18} color="var(--green)"/>
            <div>
              <div style={{ fontSize:14, fontWeight:600, color:"var(--green)" }}>Model STABLE</div>
              <div style={{ fontSize:12, color:"rgba(255,255,255,0.5)" }}>Version: credo-mlp-v2.3 · Cập nhật lần cuối: 01/05/2026 · 267 features</div>
            </div>
          </div>
          <button style={{ display:"flex", alignItems:"center", gap:6, padding:"7px 14px", background:"rgba(0,0,0,0.2)", border:"1px solid rgba(255,255,255,0.1)", borderRadius:8, color:"#fff", cursor:"pointer", fontSize:12 }}>
            <RefreshCw size={13}/> Kiểm tra lại
          </button>
        </div>

        {/* Metrics grid */}
        <div style={{ display:"grid", gridTemplateColumns:"repeat(4,1fr)", gap:16, marginBottom:20 }}>
          {metrics.map(m => (
            <div key={m.label} style={{ background:"var(--bg-card)", border:"1px solid var(--border)", borderRadius:"var(--radius)", padding:20 }}>
              <div style={{ fontSize:12, color:"var(--text-muted)", marginBottom:8 }}>{m.label}</div>
              <div style={{ fontSize:28, fontWeight:700, color: m.good?"var(--green)":"var(--red)" }}>{m.value}</div>
              <div style={{ display:"flex", alignItems:"center", justifyContent:"space-between", marginTop:8 }}>
                <div style={{ fontSize:11, color:"var(--text-muted)" }}>Ngưỡng: {m.threshold}</div>
                <div style={{ display:"flex", alignItems:"center", gap:3, fontSize:11, color:"var(--green)" }}>
                  <TrendingUp size={11}/> vs tháng trước
                </div>
              </div>
            </div>
          ))}
        </div>

        <div style={{ display:"grid", gridTemplateColumns:"1fr 1fr", gap:16, marginBottom:20 }}>

          {/* Gini trend */}
          <div style={{ background:"var(--bg-card)", border:"1px solid var(--border)", borderRadius:"var(--radius)", padding:20 }}>
            <div style={{ fontSize:14, fontWeight:600, marginBottom:4 }}>Gini & KS theo thời gian</div>
            <div style={{ fontSize:12, color:"var(--text-muted)", marginBottom:16 }}>7 tháng gần nhất</div>
            <div style={{ display:"flex", gap:4, marginBottom:8 }}>
              <span style={{ fontSize:11, color:"var(--blue)" }}>● Gini</span>
              <span style={{ fontSize:11, color:"var(--green)", marginLeft:12 }}>● KS</span>
            </div>
            <svg width="100%" height={100} viewBox="0 0 280 100" preserveAspectRatio="xMidYMid meet">
              {/* Gini */}
              {giniData.map((v,i) => {
                if(i===0) return null;
                const x1=(i-1)/(giniData.length-1)*280, x2=i/(giniData.length-1)*280;
                const y1=100-(( giniData[i-1]-0.48)/(0.62-0.48))*80;
                const y2=100-((v-0.48)/(0.62-0.48))*80;
                return <line key={`g${i}`} x1={x1} y1={y1} x2={x2} y2={y2} stroke="var(--blue)" strokeWidth={2}/>;
              })}
              {/* KS */}
              {ksData.map((v,i) => {
                if(i===0) return null;
                const x1=(i-1)/(ksData.length-1)*280, x2=i/(ksData.length-1)*280;
                const y1=100-((ksData[i-1]-0.32)/(0.44-0.32))*80;
                const y2=100-((v-0.32)/(0.44-0.32))*80;
                return <line key={`k${i}`} x1={x1} y1={y1} x2={x2} y2={y2} stroke="var(--green)" strokeWidth={2}/>;
              })}
              {/* Labels */}
              {months.map((m,i) => (
                <text key={m} x={i/(months.length-1)*280} y={98} textAnchor="middle" fill="var(--text-muted)" fontSize={9}>{m}</text>
              ))}
            </svg>
          </div>

          {/* PSI */}
          <div style={{ background:"var(--bg-card)", border:"1px solid var(--border)", borderRadius:"var(--radius)", padding:20 }}>
            <div style={{ fontSize:14, fontWeight:600, marginBottom:4 }}>PSI Monitor</div>
            <div style={{ fontSize:12, color:"var(--text-muted)", marginBottom:16 }}>Population Stability Index — ngưỡng cảnh báo 0.25</div>
            <div style={{ display:"flex", flexDirection:"column", gap:10 }}>
              {months.map((m,i) => {
                const v = psiData[i];
                const pct = v / 0.25 * 100;
                const color = v < 0.10 ? "var(--green)" : v < 0.25 ? "var(--orange)" : "var(--red)";
                return (
                  <div key={m} style={{ display:"grid", gridTemplateColumns:"30px 1fr 50px", alignItems:"center", gap:10 }}>
                    <div style={{ fontSize:11, color:"var(--text-muted)" }}>{m}</div>
                    <div style={{ background:"var(--bg)", borderRadius:4, height:8, overflow:"hidden" }}>
                      <div style={{ width:`${pct}%`, height:"100%", background:color, borderRadius:4, transition:"width 0.5s" }}/>
                    </div>
                    <div style={{ fontSize:12, fontWeight:600, color, textAlign:"right" }}>{v.toFixed(2)}</div>
                  </div>
                );
              })}
            </div>
            <div style={{ display:"flex", gap:12, marginTop:12, fontSize:11 }}>
              <span style={{ color:"var(--green)" }}>● Ổn định (&lt;0.10)</span>
              <span style={{ color:"var(--orange)" }}>● Theo dõi (0.10–0.25)</span>
              <span style={{ color:"var(--red)" }}>● Retrain (&gt;0.25)</span>
            </div>
          </div>
        </div>

        {/* Vintage analysis */}
        <div style={{ background:"var(--bg-card)", border:"1px solid var(--border)", borderRadius:"var(--radius)" }}>
          <div style={{ padding:"16px 20px", borderBottom:"1px solid var(--border)", display:"flex", alignItems:"center", gap:8 }}>
            <div style={{ fontSize:14, fontWeight:600 }}>Vintage Analysis</div>
            <span style={{ fontSize:12, color:"var(--text-muted)" }}>— Tỷ lệ default thực tế theo cohort giải ngân</span>
          </div>
          <table style={{ width:"100%", borderCollapse:"collapse" }}>
            <thead>
              <tr style={{ borderBottom:"1px solid var(--border)", background:"var(--bg)" }}>
                {["Cohort","Số hồ sơ","Default 3 tháng","Default 6 tháng","Default 12 tháng","Trạng thái"].map(h => (
                  <th key={h} style={{ padding:"10px 20px", textAlign:"left", fontSize:12, color:"var(--text-muted)", fontWeight:500 }}>{h}</th>
                ))}
              </tr>
            </thead>
            <tbody>
              {vintageData.map((row,i) => (
                <tr key={row.cohort} style={{ borderBottom: i<vintageData.length-1?"1px solid var(--border)":"none" }}>
                  <td style={{ padding:"12px 20px", fontSize:13, fontWeight:600 }}>{row.cohort}</td>
                  <td style={{ padding:"12px 20px", fontSize:13 }}>{row.size.toLocaleString()}</td>
                  <td style={{ padding:"12px 20px", fontSize:13, color:"var(--green)" }}>{row.default3m}</td>
                  <td style={{ padding:"12px 20px", fontSize:13, color: row.default6m==="—"?"var(--text-muted)":"var(--orange)" }}>{row.default6m}</td>
                  <td style={{ padding:"12px 20px", fontSize:13, color: row.default12m==="—"?"var(--text-muted)":"var(--red)" }}>{row.default12m}</td>
                  <td style={{ padding:"12px 20px" }}>
                    <span style={{
                      background: row.default12m!=="—"?"var(--green-dim)":"var(--blue-dim)",
                      color: row.default12m!=="—"?"var(--green)":"var(--blue)",
                      fontSize:11, padding:"2px 8px", borderRadius:20, fontWeight:600,
                    }}>
                      {row.default12m !== "—" ? "Hoàn thành" : "Đang theo dõi"}
                    </span>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}