"use client";
import { useState } from "react";
import Header from "@/components/layout/Header";
import Link from "next/link";
import { PlusCircle, Search, Filter, Eye } from "lucide-react";

const apps = [
  { id:"APP-2026-0048", name:"Nguyễn Văn An",   cmnd:"079201012345", income:20_000_000, amount:120_000_000, score:712, group:"B", decision:"APPROVED",      status:"COMPLETED", date:"29/05/2026 14:32" },
  { id:"APP-2026-0047", name:"Trần Thị Bích",   cmnd:"031185067890", income:15_000_000, amount:80_000_000,  score:634, group:"C", decision:"CONDITIONAL",   status:"COMPLETED", date:"29/05/2026 14:18" },
  { id:"APP-2026-0046", name:"Lê Minh Khoa",    cmnd:"001199054321", income:12_000_000, amount:50_000_000,  score:428, group:"E", decision:"REJECTED",      status:"COMPLETED", date:"29/05/2026 13:55" },
  { id:"APP-2026-0045", name:"Phạm Thị Lan",    cmnd:"052192078654", income:18_000_000, amount:90_000_000,  score:521, group:"D", decision:"MANUAL_REVIEW", status:"PENDING",   date:"29/05/2026 13:40" },
  { id:"APP-2026-0044", name:"Hoàng Văn Dũng",  cmnd:"048188034567", income:35_000_000, amount:200_000_000, score:775, group:"A", decision:"APPROVED",      status:"COMPLETED", date:"29/05/2026 13:22" },
  { id:"APP-2026-0043", name:"Vũ Thị Mai",      cmnd:"079195023456", income:22_000_000, amount:100_000_000, score:688, group:"B", decision:"APPROVED",      status:"COMPLETED", date:"29/05/2026 13:05" },
  { id:"APP-2026-0042", name:"Đinh Văn Long",   cmnd:"036190045678", income:9_000_000,  amount:30_000_000,  score:465, group:"E", decision:"REJECTED",      status:"COMPLETED", date:"29/05/2026 12:48" },
  { id:"APP-2026-0041", name:"Đỗ Văn Hùng",     cmnd:"025187056789", income:16_000_000, amount:70_000_000,  score:498, group:"D", decision:"MANUAL_REVIEW", status:"PENDING",   date:"29/05/2026 12:30" },
];

const groupColors: Record<string, {color:string;bg:string}> = {
  A:{color:"var(--green)", bg:"var(--green-dim)"},
  B:{color:"var(--blue)",  bg:"var(--blue-dim)" },
  C:{color:"var(--orange)",bg:"var(--orange-dim)"},
  D:{color:"#f59e0b",      bg:"#451a03"         },
  E:{color:"var(--red)",   bg:"var(--red-dim)"  },
};
const decisionColor: Record<string,string> = {
  APPROVED:"var(--green)", REJECTED:"var(--red)",
  CONDITIONAL:"var(--orange)", MANUAL_REVIEW:"#a78bfa",
};
const decisionLabel: Record<string,string> = {
  APPROVED:"Phê duyệt", REJECTED:"Từ chối",
  CONDITIONAL:"Có điều kiện", MANUAL_REVIEW:"Xem xét thủ công",
};
const tabs = ["Tất cả","Đã duyệt","Từ chối","Xem xét thủ công","Đang xử lý"];

export default function ApplicationsPage() {
  const [activeTab, setActiveTab] = useState("Tất cả");
  const [search, setSearch] = useState("");

  const filtered = apps.filter(a => {
    const matchTab =
      activeTab === "Tất cả" ? true :
      activeTab === "Đã duyệt" ? a.decision === "APPROVED" :
      activeTab === "Từ chối" ? a.decision === "REJECTED" :
      activeTab === "Xem xét thủ công" ? a.decision === "MANUAL_REVIEW" :
      a.status === "PENDING";
    const matchSearch = a.name.toLowerCase().includes(search.toLowerCase()) || a.cmnd.includes(search) || a.id.includes(search);
    return matchTab && matchSearch;
  });

  return (
    <div>
      <Header title="Quản lý hồ sơ vay" subtitle={`${apps.length} hồ sơ tổng cộng`} />
      <div style={{ padding: 24 }} className="animate-in">

        {/* Toolbar */}
        <div style={{ display:"flex", justifyContent:"space-between", alignItems:"center", marginBottom:20 }}>
          <div style={{ display:"flex", alignItems:"center", gap:8, background:"var(--bg-card)", border:"1px solid var(--border)", borderRadius:8, padding:"7px 12px", width:300 }}>
            <Search size={14} color="var(--text-muted)" />
            <input value={search} onChange={e=>setSearch(e.target.value)}
              placeholder="Tìm tên, CMND, mã hồ sơ..."
              style={{ background:"transparent", border:"none", outline:"none", color:"var(--text)", fontSize:13, width:"100%" }} />
          </div>
          <div style={{ display:"flex", gap:10 }}>
            <button style={{ display:"flex", alignItems:"center", gap:6, padding:"8px 14px", background:"var(--bg-card)", border:"1px solid var(--border)", borderRadius:8, color:"var(--text-dim)", cursor:"pointer", fontSize:13 }}>
              <Filter size={14} /> Lọc
            </button>
            <Link href="/applications/new" style={{ textDecoration:"none" }}>
              <button style={{ display:"flex", alignItems:"center", gap:6, padding:"8px 16px", background:"var(--blue)", border:"none", borderRadius:8, color:"#fff", cursor:"pointer", fontSize:13, fontWeight:600 }}>
                <PlusCircle size={14} /> Tạo hồ sơ mới
              </button>
            </Link>
          </div>
        </div>

        {/* Tabs */}
        <div style={{ display:"flex", gap:4, marginBottom:16, borderBottom:"1px solid var(--border)", paddingBottom:0 }}>
          {tabs.map(tab => (
            <button key={tab} onClick={()=>setActiveTab(tab)} style={{
              padding:"8px 16px", background:"transparent", border:"none",
              borderBottom: activeTab===tab ? "2px solid var(--blue)" : "2px solid transparent",
              color: activeTab===tab ? "var(--blue)" : "var(--text-muted)",
              cursor:"pointer", fontSize:13, fontWeight: activeTab===tab ? 600 : 400,
              marginBottom:-1, transition:"all 0.15s",
            }}>{tab}</button>
          ))}
        </div>

        {/* Table */}
        <div style={{ background:"var(--bg-card)", border:"1px solid var(--border)", borderRadius:"var(--radius)", overflow:"hidden" }}>
          <table style={{ width:"100%", borderCollapse:"collapse" }}>
            <thead>
              <tr style={{ borderBottom:"1px solid var(--border)", background:"var(--bg)" }}>
                {["Mã hồ sơ","Khách hàng","Thu nhập","Số tiền vay","Credit Score","Nhóm","Quyết định","Ngày nộp",""].map(h=>(
                  <th key={h} style={{ padding:"11px 16px", textAlign:"left", fontSize:12, color:"var(--text-muted)", fontWeight:500 }}>{h}</th>
                ))}
              </tr>
            </thead>
            <tbody>
              {filtered.map((app,i) => {
                const g = groupColors[app.group];
                return (
                  <tr key={app.id} style={{ borderBottom: i<filtered.length-1?"1px solid var(--border)":"none" }}>
                    <td style={{ padding:"13px 16px", fontSize:12, color:"var(--blue-light)", fontFamily:"monospace" }}>{app.id}</td>
                    <td style={{ padding:"13px 16px" }}>
                      <div style={{ fontSize:13, fontWeight:500 }}>{app.name}</div>
                      <div style={{ fontSize:11, color:"var(--text-muted)", fontFamily:"monospace" }}>{app.cmnd}</div>
                    </td>
                    <td style={{ padding:"13px 16px", fontSize:13, color:"var(--text-dim)" }}>{(app.income/1_000_000).toFixed(0)}tr</td>
                    <td style={{ padding:"13px 16px", fontSize:13, fontWeight:500 }}>{(app.amount/1_000_000).toFixed(0)}tr</td>
                    <td style={{ padding:"13px 16px", fontSize:14, fontWeight:700 }}>{app.score}</td>
                    <td style={{ padding:"13px 16px" }}>
                      <span style={{ background:g.bg, color:g.color, padding:"2px 10px", borderRadius:20, fontSize:12, fontWeight:700 }}>
                        {app.group}
                      </span>
                    </td>
                    <td style={{ padding:"13px 16px" }}>
                      <span style={{ color:decisionColor[app.decision], fontSize:12, fontWeight:500 }}>
                        {decisionLabel[app.decision]}
                      </span>
                    </td>
                    <td style={{ padding:"13px 16px", fontSize:12, color:"var(--text-muted)" }}>{app.date}</td>
                    <td style={{ padding:"13px 16px" }}>
                      <Link href={`/applications/${app.id}`} style={{ textDecoration:"none" }}>
                        <button style={{ display:"flex", alignItems:"center", gap:4, padding:"5px 10px", background:"var(--bg)", border:"1px solid var(--border)", borderRadius:6, color:"var(--text-dim)", cursor:"pointer", fontSize:12 }}>
                          <Eye size={12}/> Xem
                        </button>
                      </Link>
                    </td>
                  </tr>
                );
              })}
            </tbody>
          </table>
          {filtered.length === 0 && (
            <div style={{ textAlign:"center", padding:40, color:"var(--text-muted)" }}>Không có hồ sơ nào</div>
          )}
        </div>
      </div>
    </div>
  );
}