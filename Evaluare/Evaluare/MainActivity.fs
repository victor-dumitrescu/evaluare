namespace Evaluare

open System
open Android.App
open Android.Content
open Android.OS
open Android.Runtime
open Android.Views
open Android.Widget


type Sex = M | F
type Efort = Mediu | Intens

[<Activity(Label = "Evaluare", MainLauncher = true)>]
type MainActivity() = 
    inherit Activity()

    let mutable sex: Sex = M
    let mutable efort: Efort = Mediu

    let rma_text =  "Rata metabolică activă: "
    let rmt_text =  "Rata metabolică totală: "
    let m500_text = "Slăbire:                               "
    let p500_text = "Creșterea greutății:          "
    let proteic_text = "Factor proteic:      "

    override this.OnCreate(bundle) = 
        base.OnCreate(bundle)
        this.SetContentView(Resource_Layout.Main)

        let out_rma = this.FindViewById<TextView>(Resource_Id.rma)
        let out_rmt = this.FindViewById<TextView>(Resource_Id.rmt)
        let m500 = this.FindViewById<TextView>(Resource_Id.m500)
        let p500 = this.FindViewById<TextView>(Resource_Id.p500)

        let efort_togglebutton = this.FindViewById<ToggleButton>(Resource_Id.efort_togglebutton)
        let sex_togglebutton = this.FindViewById<ToggleButton>(Resource_Id.sex_togglebutton)

        efort_togglebutton.Click.Add (fun (e: EventArgs) -> if efort_togglebutton.Checked then 
                                                                 efort <- Intens
                                                            else
                                                                 efort <- Mediu )

        sex_togglebutton.Click.Add (fun (e: EventArgs) -> if sex_togglebutton.Checked then 
                                                                 sex <- F
                                                            else
                                                                 sex <- M )

        let rmb_edittext = this.FindViewById<EditText>(Resource_Id.rmb)
        let efort_edittext = this.FindViewById<EditText>(Resource_Id.hEfort)
        rmb_edittext.KeyPress.Add (fun (e: View.KeyEventArgs ) -> 
                                    e.Handled <- false
                                    if ((e.KeyCode = Keycode.Enter) && (e.Event.Action = KeyEventActions.Down)) then
                                        Toast.MakeText(this, rmb_edittext.Text, ToastLength.Short).Show()
                                        e.Handled <- true
                                 )

        let button = this.FindViewById<Button>(Resource_Id.myButton)
        button.Click.Add(fun args -> 
            if not (String.IsNullOrEmpty rmb_edittext.Text || String.IsNullOrEmpty efort_edittext.Text) then
                let multiplier = match sex with
                                 | M -> 1.3
                                 | F -> 1.25
                let rma = multiplier * float rmb_edittext.Text
                out_rma.Text <- sprintf "%s %d" rma_text (int rma)

                let tip_efort = match efort with
                                | Mediu -> 200.0 / 7.0
                                | Intens -> 300.0 / 7.0
                let rmt = int (rma + tip_efort * float efort_edittext.Text)
                out_rmt.Text <- sprintf "%s %d" rmt_text rmt
                m500.Text <- sprintf "%s %d" m500_text (rmt - 500)
                p500.Text <- sprintf "%s %d" p500_text (rmt + 500)
            else
                Toast.MakeText(this, "Date incomplete", ToastLength.Short).Show()
            )

        let greutate_edittext = this.FindViewById<EditText>(Resource_Id.greutatea)
        let grasime_edittext = this.FindViewById<EditText>(Resource_Id.grasime)
        let mmusculara_edittext = this.FindViewById<EditText>(Resource_Id.m_musculara)
        let out_grasime = this.FindViewById<TextView>(Resource_Id.grasime_out)
        let out_mmusculara = this.FindViewById<TextView>(Resource_Id.m_musculara_out)

        let button3 = this.FindViewById<Button>(Resource_Id.myButton3)
        button3.Click.Add(fun args -> 
                if not (String.IsNullOrEmpty greutate_edittext.Text || String.IsNullOrEmpty grasime_edittext.Text || String.IsNullOrEmpty mmusculara_edittext.Text) then
                    match float grasime_edittext.Text with
                    | p when p >= 0.0 && p <= 100.0 -> out_grasime.Text <- sprintf "%s %d" "Grăsime (kg):             " (p * (float greutate_edittext.Text) / 100.0 |> int)
                    | _ -> Toast.MakeText(this, "Procentajul de grăsime trebuie să fie în intervalul 0 - 100", ToastLength.Long).Show()
                    out_mmusculara.Text <- sprintf "%s %d" "% Masă musculară: " ((float mmusculara_edittext.Text) * 100.0 / (float greutate_edittext.Text) |> int)
                else
                    Toast.MakeText(this, "Date incomplete", ToastLength.Short).Show()
                )

        let imc_edittext = this.FindViewById<EditText>(Resource_Id.imc)
        let height_edittext = this.FindViewById<EditText>(Resource_Id.height)
        let out_proteic = this.FindViewById<TextView>(Resource_Id.proteic)

        let button2 = this.FindViewById<Button>(Resource_Id.myButton2)
        button2.Click.Add(fun args -> 
                if not (String.IsNullOrEmpty imc_edittext.Text || String.IsNullOrEmpty height_edittext.Text) then
                    match Protein.computeProteins (int imc_edittext.Text, int height_edittext.Text, (if sex = M then 1 else 0)) with
                    | Some p -> 
                            out_proteic.Text <- sprintf "%s %d" proteic_text p
                    | None -> 
                        Toast.MakeText(this, "Factorul proteic nu a putut fi calculat - Date invalide", ToastLength.Long).Show()
                else
                    Toast.MakeText(this, "Date incomplete", ToastLength.Short).Show()
                )