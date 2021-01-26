[<EntryPoint>]
let main argv =
    System.Console.OutputEncoding <- System.Text.Encoding.UTF8
    System.Console.WindowHeight <- 50
    System.Console.WindowWidth <- 130
    printfn "Welcome to the FHTW Domain REPL!"
    printfn "Please enter your commands to interact with the system."
    printfn "Press CTRL+C to stop the program."
    printfn "A bad clone of"

    printfn "
                                                                    .;1.
                                                                   .i11i
                                                                  .i111t:
                                                                 ,i11tt11,
                                                                ,i11f;tt11.
                                                               ,111f;;ft1t:
                                                .,;;.;i       .111fLftt1;.          .,,,,,,.
                   .,:::::,.                 ,:i11ti:11i.    .i11tft1i;.     .,,,,,;1t1111t1
               .:ii111111111i:.          .,;i111111ii1111,  .;:i11111:.    .;111111111111111.       ,:.
            .:i1111111111111111:       .,i11111tt11i11t111:  ,:i1111111i;. :i111111111tfff11,     .:;t11i;:.
          .:11111ttfffffffft1111;      :;i1tff;;f1111fft111;.:111111111111::;11fffft11fLLf1t:     .;;111ttt11i;.
         :1111tf;;;LLLLLLLLfft111:     :;i1ffLLLf111fLLftt111111tffffftt111ii1tLLLLt11ffLf1t;     .;i1tftt111tt::1i:,.
       .i111ff;;LLLLLLLLLLLLLft111.    :ii111ffLft1tLffLLft111t;;;;L;;Lftt1111tLfLLf1tLLLft1i     .ii1tffffft11iitttt1i;:.
      .:;11f;LLLLLLLLLLLLLLLfLft11;    :11111fLLf1tLfLLfLLt11f;LfttttfLL;t1111fLLLLf1tLfLft11     .ii111LLLLft1111t111ttt;
      .::i1tLfLLLLLLLLL;fffL;fLf11i    :;::i1fLLftfLLLL;ft11tLLf11i11tLLt11i11fLLLLfttLLLLt11. ,:;i1i;itfLLLft111tfffft11.
       ,;;11tLfLLLLLLLft1111f;Lft1i   .,.::i1fLLffLLLLLft111fL;t1..it;;t11i:11fLfLLLtfLLLLt11;i1ttt1111tLfLLLt111fLLLLt1i
        ;;i11fLfLLLLLLf11i111fLft1i.:i1111111tLfLLLLLLft1i11fL;ti.it;Lt111ii1tLLLLLLfLLLLLf1111tttttt111fLLLLf111fLfLf1t:
        .i;i1tt1fLfLLLLt1:.11fLf11ii11ttt1111tLfLLLLLf11: 11;Lft1it;Lt11t1111tLfLLLLLLLLfLf111ffttfLftt11fLfLft1tfLfLf11.
         :ii11111LLLLLLf1i.i1fLf1111t;ftfftt11fLLLLLf111;,11ffLf1tfLt1tfft111tLLLLLLLLLLLLt1tff11fLffftt1tLfLft1tLfLftti
          iii1i11fLfLLLf11;itLLt11tf;t1fLLLft11fLLLLtt111111fLLff;LftfLLLft111tLLffLLLLLLf11fLt11fLLtfft1tLfLft1fLfLf1t,
          ,1iiii11fLLLLLt111fLf11tLLtitLLftff11tLfLLffftt1111fLLLLLfLLLfLLf111fLLttLfLffLt1tLf1111tt1fLt1tfLfLt1fLfLt11
           :,,i;11fLfLfLf1tfLf111fLf111tff1fft1tLLLLLLLLfft11tfLLLLLLLLLft111fLLft1fLLtff11fLft11111tfLt1tLffLftLfLftt;
              ;;i11LLLLLff;Lf111tLLf1111111fft1tLfttfLLLLLft111tfLLL;fft1111tLfLft1tLf1ff11ffLft111tfLLt1tLtfLffLf;f1t,
              .;;11fLfLLLLft1111tLLft11111fLft1tLf111tfLLLLtt11111ttt1111ii1tLfLf111ft1tf11fLLLfftfLLLft1ff1fLLLLLLt11
               :;i11LLLLLft11ii1fLfLfttttfLLft1tLf11111tffLLftt1111111i;::i1fLLLf111tt1tf11tLfLLLLLLfLt1tLf1fLLLLLf1t;
               .;;11fLLLLf111;i1tLfLLLffLLfLf11fLf11i;i111tfLLfftt11:,. ,:11ttfft1111111ft11fLLLLLLLLf11fLt1fLLLfLf11.
                ::;11LLLLft11;;11LfLLLLLLfLLt1tLLf11;:;;i111tfLLLt11.   :;11111111ii1111ft111fL;;L;ft11fLft1fLLLLLt1i
                .::11fLLLLt11i:i1tLLLLLLLL;t11tLLf11: ,:;;i111tfLt11.   ::;;;iii11;;1ii1fft111ttttt111fLLft1fLLLLf1t:
                 ::;11LLfLf11i::11tfLL;;Lf1111tLL;11:   ,:;;i111tt11.      ..,,::.:;i;i1tLfft111111ii1fLLf11fLLLLt11.
                 .::11fLfLft11::;111tttt111i;1tfft11:     .:;;;i1111.             .,,;i11tttt11i:,,:i1ffLf11fLfLftti
                  ::;11LLLft11,,::ii11111i:::1111111:       .::;;i11.               .;;i11111111  ,;i111tt11fLfLf1t,
                  .::11fLfLf11; .:::;;;;,  ::iii;;:.          .,:;;i                 ,,::;;iii11. ,;iii11111fLfLt11
                   ::;11LfLft1i   ..,..    ,,...                 ,,                      ..,,::.  ..,,::;;i1fLLftt;
                   .::11f;ff111,                                                                        .;itttff1t,
                    ::;11t11111,                                                                        .;;111111i
                    .::1111i;:.                                                                          ,:;;ii1t:
                     ::;i;,.                                                                               .,::;:.
                     ,:..                                                                                     ..
    "
    
    let initialState = Domain.init ()
    printf "> "
    Repl.loop initialState
    0 // return an integer exit code
