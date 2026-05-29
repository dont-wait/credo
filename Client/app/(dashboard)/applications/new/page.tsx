"use client";
import { useState } from "react";
import Header from "@/components/layout/Header";
import { ChevronRight, User, Briefcase, Home, CreditCard, Upload, AlertCircle } from "lucide-react";

const steps = [
  { id:1, label:"Thông tin cá nhân",  icon: User      },
  { id:2, label:"Việc làm & Thu nhập",icon: Briefcase },
  { id:3, label:"Thông tin cư trú",   icon: Home      },
  { id:4, label:"Khoản vay",          icon: CreditCard},
  { id:5, label:"Tài liệu",           icon: Upload    },
];

function InputField({ label, placeholder, type="text", required=false, hint="" }:
  { label:string; placeholder:string; type?:string; required?:boolean; hint?:string }) {
  return (
    <div style={{ marginBottom: 16 }}>
      <label style={{ display:"block", fontSize:12, fontWeight:500, color:"var(--text-dim)", marginBottom:6 }}>
        {label}{required && <span style={{ color:"var(--red)", marginLeft:2 }}>*</span>}
      </label>
      <input type={type} placeholder={placeholder} style={{
        width:"100%", padding:"9px 12px",
        background:"var(--bg)", border:"1px solid var(--border)",
        borderRadius:8, color:"var(--text)", fontSize:13, outline:"none",
      }}
        onFocus={e=>(e.target.style.borderColor="var(--blue)")}
        onBlur={e=>(e.target.style.borderColor="var(--border)")}
      />
      {hint && <div style={{ fontSize:11, color:"var(--text-muted)", marginTop:4 }}>{hint}</div>}
    </div>
  );
}

function SelectField({ label, options, required=false }:
  { label:string; options:string[]; required?:boolean }) {
  return (
    <div style={{ marginBottom:16 }}>
      <label style={{ display:"block", fontSize:12, fontWeight:500, color:"var(--text-dim)", marginBottom:6 }}>
        {label}{required && <span style={{ color:"var(--red)", marginLeft:2 }}>*</span>}
      </label>
      <select style={{
        width:"100%", padding:"9px 12px",
        background:"var(--bg)", border:"1px solid var(--border)",
        borderRadius:8, color:"var(--text)", fontSize:13, outline:"none", cursor:"pointer",
      }}>
        <option value="">Chọn...</option>
        {options.map(o=><option key={o} value={o}>{o}</option>)}
      </select>
    </div>
  );
}

export default function NewApplicationPage() {
  const [step, setStep] = useState(1);

  return (
    <div>
      <Header title="Tạo hồ sơ vay mới" subtitle="Điền đầy đủ thông tin để bắt đầu đánh giá tín dụng" />
      <div style={{ padding:24 }} className="animate-in">
        <div style={{ display:"grid", gridTemplateColumns:"260px 1fr", gap:20 }}>

          {/* Steps sidebar */}
          <div style={{ background:"var(--bg-card)", border:"1px solid var(--border)", borderRadius:"var(--radius)", padding:20, height:"fit-content" }}>
            <div style={{ fontSize:12, color:"var(--text-muted)", marginBottom:16, fontWeight:500 }}>TIẾN TRÌNH</div>
            {steps.map((s,i) => {
              const done = step > s.id;
              const active = step === s.id;
              const Icon = s.icon;
              return (
                <div key={s.id} style={{ position:"relative" }}>
                  <div style={{
                    display:"flex", alignItems:"center", gap:10, padding:"10px 12px",
                    borderRadius:8, marginBottom:4, cursor: done||active?"pointer":"default",
                    background: active?"var(--blue-dim)":"transparent",
                    color: active?"var(--blue-light)": done?"var(--green)":"var(--text-muted)",
                  }} onClick={()=>done&&setStep(s.id)}>
                    <div style={{
                      width:28, height:28, borderRadius:"50%", flexShrink:0,
                      display:"flex", alignItems:"center", justifyContent:"center", fontSize:12, fontWeight:700,
                      background: done?"var(--green-dim)": active?"var(--blue-dim)":"var(--bg)",
                      border: `1px solid ${done?"var(--green)":active?"var(--blue)":"var(--border)"}`,
                      color: done?"var(--green)":active?"var(--blue)":"var(--text-muted)",
                    }}>
                      {done ? "✓" : s.id}
                    </div>
                    <div>
                      <div style={{ fontSize:13, fontWeight: active?600:400 }}>{s.label}</div>
                    </div>
                  </div>
                  {i < steps.length-1 && (
                    <div style={{ width:1, height:8, background:"var(--border)", marginLeft:24, marginBottom:4 }}/>
                  )}
                </div>
              );
            })}
          </div>

          {/* Form content */}
          <div style={{ background:"var(--bg-card)", border:"1px solid var(--border)", borderRadius:"var(--radius)", padding:28 }}>

            {/* Note */}
            <div style={{ display:"flex", gap:10, background:"var(--blue-dim)", border:"1px solid rgba(59,130,246,0.3)", borderRadius:8, padding:"10px 14px", marginBottom:24 }}>
              <AlertCircle size={16} color="var(--blue)" style={{ flexShrink:0, marginTop:2 }} />
              <div style={{ fontSize:12, color:"var(--blue-light)" }}>
                Hệ thống sẽ tự động tra cứu CIC sau khi hồ sơ được tạo. Vui lòng điền chính xác CMND/CCCD.
              </div>
            </div>

            {/* Step 1 */}
            {step === 1 && (
              <>
                <div style={{ fontSize:15, fontWeight:700, marginBottom:20, color:"var(--text)" }}>Thông tin cá nhân</div>
                <div style={{ display:"grid", gridTemplateColumns:"1fr 1fr", gap:"0 20px" }}>
                  <InputField label="Họ và tên" placeholder="Nguyễn Văn A" required />
                  <InputField label="CMND / CCCD" placeholder="012345678901" required hint="12 số, không dấu cách" />
                  <InputField label="Ngày sinh" placeholder="" type="date" required />
                  <SelectField label="Giới tính" options={["Nam","Nữ","Khác"]} required />
                  <InputField label="Số điện thoại" placeholder="0901234567" required />
                  <InputField label="Email" placeholder="example@email.com" type="email" />
                </div>
              </>
            )}

            {/* Step 2 */}
            {step === 2 && (
              <>
                <div style={{ fontSize:15, fontWeight:700, marginBottom:20 }}>Việc làm & Thu nhập</div>
                <div style={{ display:"grid", gridTemplateColumns:"1fr 1fr", gap:"0 20px" }}>
                  <InputField label="Nơi làm việc" placeholder="Công ty TNHH ABC" required />
                  <SelectField label="Loại hợp đồng" options={["Hợp đồng chính thức (dài hạn)","Hợp đồng thời vụ","Tự kinh doanh","Không có hợp đồng"]} required />
                  <SelectField label="Loại hình doanh nghiệp" options={["Nhà nước","Tư nhân","FDI (nước ngoài)","Hộ kinh doanh"]} required />
                  <InputField label="Số năm làm việc hiện tại" placeholder="3.5" type="number" required hint="Đơn vị: năm" />
                  <InputField label="Thu nhập hàng tháng (VNĐ)" placeholder="20,000,000" type="number" required hint="Trước thuế" />
                  <SelectField label="Nguồn thu nhập chính" options={["Lương","Kinh doanh","Đầu tư","Cho thuê tài sản","Khác"]} required />
                </div>
              </>
            )}

            {/* Step 3 */}
            {step === 3 && (
              <>
                <div style={{ fontSize:15, fontWeight:700, marginBottom:20 }}>Thông tin cư trú</div>
                <div style={{ display:"grid", gridTemplateColumns:"1fr 1fr", gap:"0 20px" }}>
                  <InputField label="Địa chỉ thường trú" placeholder="123 Đường ABC, Phường X, Quận Y" required />
                  <SelectField label="Loại nhà ở" options={["Sở hữu (nhà/đất riêng)","Thuê nhà","Ở cùng gia đình","Khác"]} required />
                  <InputField label="Thời gian cư trú hiện tại (năm)" placeholder="2.5" type="number" required />
                  <SelectField label="Tỉnh/Thành phố" options={["TP. Hồ Chí Minh","Hà Nội","Đà Nẵng","Cần Thơ","Khác"]} required />
                </div>
              </>
            )}

            {/* Step 4 */}
            {step === 4 && (
              <>
                <div style={{ fontSize:15, fontWeight:700, marginBottom:20 }}>Thông tin khoản vay</div>
                <div style={{ display:"grid", gridTemplateColumns:"1fr 1fr", gap:"0 20px" }}>
                  <InputField label="Số tiền vay (VNĐ)" placeholder="120,000,000" type="number" required />
                  <SelectField label="Mục đích vay" options={["Tiêu dùng cá nhân","Mua ô tô","Mua nhà / BĐS","Kinh doanh hộ gia đình","Du học","Y tế / Giáo dục","Khác"]} required />
                  <SelectField label="Kỳ hạn mong muốn" options={["12 tháng","24 tháng","36 tháng","48 tháng","60 tháng"]} required />
                  <SelectField label="Loại vay" options={["Tín chấp (không tài sản thế chấp)","Thế chấp (có tài sản đảm bảo)"]} required />
                  <SelectField label="Loại tài sản thế chấp (nếu có)" options={["Nhà/đất","Ô tô","Sổ tiết kiệm","Khác"]} />
                  <InputField label="Giá trị tài sản thế chấp (VNĐ)" placeholder="500,000,000" type="number" hint="Để trống nếu tín chấp" />
                </div>
              </>
            )}

            {/* Step 5 */}
            {step === 5 && (
              <>
                <div style={{ fontSize:15, fontWeight:700, marginBottom:20 }}>Upload tài liệu</div>
                {[
                  { label:"CMND/CCCD (2 mặt)", required:true,  hint:"JPG, PNG, PDF — tối đa 5MB" },
                  { label:"Sao kê lương 3-6 tháng gần nhất", required:true, hint:"PDF từ ngân hàng hoặc hình ảnh rõ ràng" },
                  { label:"Hợp đồng lao động", required:false, hint:"PDF hoặc ảnh chụp" },
                  { label:"Tài liệu tài sản thế chấp", required:false, hint:"Sổ đỏ, đăng ký xe... (nếu có)" },
                ].map(doc => (
                  <div key={doc.label} style={{ marginBottom:16 }}>
                    <label style={{ display:"block", fontSize:12, fontWeight:500, color:"var(--text-dim)", marginBottom:8 }}>
                      {doc.label}{doc.required && <span style={{ color:"var(--red)", marginLeft:2 }}>*</span>}
                    </label>
                    <div style={{
                      border:"2px dashed var(--border)", borderRadius:8, padding:"20px",
                      textAlign:"center", cursor:"pointer", background:"var(--bg)",
                      transition:"all 0.15s",
                    }}
                      onMouseEnter={e=>(e.currentTarget.style.borderColor="var(--blue)")}
                      onMouseLeave={e=>(e.currentTarget.style.borderColor="var(--border)")}
                    >
                      <Upload size={20} color="var(--text-muted)" style={{ margin:"0 auto 8px" }} />
                      <div style={{ fontSize:13, color:"var(--text-dim)" }}>Kéo thả hoặc <span style={{ color:"var(--blue)" }}>chọn file</span></div>
                      <div style={{ fontSize:11, color:"var(--text-muted)", marginTop:4 }}>{doc.hint}</div>
                    </div>
                  </div>
                ))}
              </>
            )}

            {/* Nav buttons */}
            <div style={{ display:"flex", justifyContent:"space-between", marginTop:28, paddingTop:20, borderTop:"1px solid var(--border)" }}>
              <button onClick={()=>setStep(s=>Math.max(1,s-1))} disabled={step===1} style={{
                padding:"9px 20px", background:"var(--bg)", border:"1px solid var(--border)",
                borderRadius:8, color: step===1?"var(--text-muted)":"var(--text)", cursor: step===1?"not-allowed":"pointer", fontSize:13,
              }}>← Quay lại</button>

              {step < 5 ? (
                <button onClick={()=>setStep(s=>Math.min(5,s+1))} style={{
                  display:"flex", alignItems:"center", gap:6,
                  padding:"9px 24px", background:"var(--blue)", border:"none",
                  borderRadius:8, color:"#fff", cursor:"pointer", fontSize:13, fontWeight:600,
                }}>
                  Tiếp theo <ChevronRight size={14}/>
                </button>
              ) : (
                <button style={{
                  padding:"9px 24px", background:"var(--green)", border:"none",
                  borderRadius:8, color:"#fff", cursor:"pointer", fontSize:13, fontWeight:600,
                }}>
                  ✓ Nộp hồ sơ & Chạy đánh giá
                </button>
              )}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}